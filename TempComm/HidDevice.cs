using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Microsoft.Win32.SafeHandles;

namespace HidDeviceLib
{
    /// <summary>
    /// HID Device Communication Library
    /// Communicates with USB HID devices to send system monitoring data
    /// </summary>
    public class HidDevice : IDisposable
    {
        #region Constants

        private const int MAX_DEVICES = 20;
        private const int MAX_PATH = 260;
        private const int HID_REPORT_SIZE = 65;
        private const int DEFAULT_TIMEOUT = 5000;

        #endregion

        #region Native Interop

        [DllImport("hid.dll", SetLastError = true)]
        private static extern void HidD_GetHidGuid(out Guid hidGuid);

        [DllImport("hid.dll", SetLastError = true)]
        private static extern bool HidD_GetAttributes(SafeFileHandle hidDeviceObject, ref HIDD_ATTRIBUTES attributes);

        [DllImport("hid.dll", SetLastError = true)]
        private static extern bool HidD_GetProductString(SafeFileHandle hidDeviceObject, byte[] buffer, uint bufferLength);

        [DllImport("hid.dll", SetLastError = true)]
        private static extern bool HidD_GetManufacturerString(SafeFileHandle hidDeviceObject, byte[] buffer, uint bufferLength);

        [DllImport("setupapi.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern IntPtr SetupDiGetClassDevs(
            ref Guid classGuid,
            IntPtr enumerator,
            IntPtr hwndParent,
            uint flags);

        [DllImport("setupapi.dll", SetLastError = true)]
        private static extern bool SetupDiEnumDeviceInterfaces(
            IntPtr deviceInfoSet,
            IntPtr deviceInfoData,
            ref Guid interfaceClassGuid,
            uint memberIndex,
            ref SP_DEVICE_INTERFACE_DATA deviceInterfaceData);

        [DllImport("setupapi.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool SetupDiGetDeviceInterfaceDetail(
            IntPtr deviceInfoSet,
            ref SP_DEVICE_INTERFACE_DATA deviceInterfaceData,
            IntPtr deviceInterfaceDetailData,
            uint deviceInterfaceDetailDataSize,
            out uint requiredSize,
            IntPtr deviceInfoData);

        [DllImport("setupapi.dll", SetLastError = true)]
        private static extern bool SetupDiDestroyDeviceInfoList(IntPtr deviceInfoSet);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern SafeFileHandle CreateFile(
            string lpFileName,
            uint dwDesiredAccess,
            uint dwShareMode,
            IntPtr lpSecurityAttributes,
            uint dwCreationDisposition,
            uint dwFlagsAndAttributes,
            IntPtr hTemplateFile);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool WriteFile(
            SafeFileHandle hFile,
            byte[] lpBuffer,
            uint nNumberOfBytesToWrite,
            out uint lpNumberOfBytesWritten,
            IntPtr lpOverlapped);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool ReadFile(
            SafeFileHandle hFile,
            byte[] lpBuffer,
            uint nNumberOfBytesToRead,
            out uint lpNumberOfBytesRead,
            IntPtr lpOverlapped);

        [StructLayout(LayoutKind.Sequential)]
        private struct HIDD_ATTRIBUTES
        {
            public uint Size;
            public ushort VendorID;
            public ushort ProductID;
            public ushort VersionNumber;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct SP_DEVICE_INTERFACE_DATA
        {
            public uint cbSize;
            public Guid InterfaceClassGuid;
            public uint Flags;
            public IntPtr Reserved;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct SP_DEVICE_INTERFACE_DETAIL_DATA
        {
            public uint cbSize;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string DevicePath;
        }

        private const uint DIGCF_PRESENT = 0x02;
        private const uint DIGCF_DEVICEINTERFACE = 0x10;
        private const uint GENERIC_READ = 0x80000000;
        private const uint GENERIC_WRITE = 0x40000000;
        private const uint FILE_SHARE_READ = 0x01;
        private const uint FILE_SHARE_WRITE = 0x02;
        private const uint OPEN_EXISTING = 3;
        private const uint FILE_ATTRIBUTE_NORMAL = 0x80;

        #endregion

        #region USB Filter

        private class UsbFilter
        {
            public int VendorID { get; set; } = -1;
            public int ProductID { get; set; } = -1;
            public int VersionNumber { get; set; } = -1;
        }

        #endregion

        #region HID Context

        private class HidContext : IDisposable
        {
            public string DevicePath { get; set; }
            public SafeFileHandle ReadHandle { get; set; }
            public SafeFileHandle WriteHandle { get; set; }
            public int ReadTimeout { get; set; } = DEFAULT_TIMEOUT;
            public int WriteTimeout { get; set; } = DEFAULT_TIMEOUT;
            public int LastError { get; set; }
            public bool IsGamdias { get; set; } = false;
            public bool IsInitialized { get; set; } = false;

            public void Dispose()
            {
                ReadHandle?.Dispose();
                WriteHandle?.Dispose();
                ReadHandle = null;
                WriteHandle = null;
            }
        }

        #endregion

        #region Private Fields

        private readonly object _lock = new object();
        private readonly List<UsbFilter> _usbFilters = new List<UsbFilter>();
        private readonly Dictionary<int, HidContext> _devices = new Dictionary<int, HidContext>();
        private int _maxDevices = 20;
        private bool _debugLogging = false;
        private bool _disposed = false;

        // System info storage
        private ushort _cpuUsage;
        private ushort _cpuFrequency;
        private ushort _cpuTemp;
        private ushort _cpuFanRpm;
        private ushort _cpuVoltage;
        private ushort _cpuWatt;

        private ushort _gpuUsage;
        private ushort _gpuFrequency;
        private ushort _gpuTemp;
        private ushort _gpuFanRpm;
        private ushort _gpuVoltage;
        private ushort _gpuWatt;

        private ushort _memUsage;
        private ushort _memFrequency;
        private ushort _memTemp;

        private ushort _diskUsage;
        private ushort _diskTemp;

        private int _netUpload;
        private int _netDownload;

        private byte _displayMode;

        private byte _reserved1;
        private byte _reserved2;
        private byte _reserved3;
        private byte _reserved4;

        #endregion

        #region Constructor

        public HidDevice()
        {
            // Default USB filter: VID=0x0145, PID=0x1005 for LK Digital Display
            _usbFilters.Add(new UsbFilter { VendorID = 0x0145, ProductID = 0x1005 });

            // Check for debug logging config
            try
            {
                string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.ini");
                if (File.Exists(configPath))
                {
                    string content = File.ReadAllText(configPath);
                    if (content.Contains("IsLog=True"))
                        _debugLogging = true;
                }
            }
            catch { }

            DebugLog("=== HidDevice Library Initialized ===");
            DebugLog("Process: " + System.Diagnostics.Process.GetCurrentProcess().ProcessName);
            DebugLog("64-bit process: " + Environment.Is64BitProcess);
            DebugLog("Base directory: " + AppDomain.CurrentDomain.BaseDirectory);
        }

        #endregion

        #region Debug Logging

        private void DebugLog(string message)
        {
            if (!_debugLogging) return;

            try
            {
                string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "hid_debug.txt");
                string timestamp = DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss.fff] ");
                File.AppendAllText(logPath, timestamp + message + Environment.NewLine);
            }
            catch { }
        }

        private void DebugLog(string format, params object[] args)
        {
            DebugLog(string.Format(format, args));
        }

        #endregion

        #region Device Enumeration

        /// <summary>
        /// List ALL HID devices on the system (for diagnostics)
        /// </summary>
        public void ListAllHidDevices()
        {
            DebugLog("=== Listing ALL HID devices ===");
            
            HidD_GetHidGuid(out Guid hidGuid);
            DebugLog("HID GUID: " + hidGuid.ToString());
            
            IntPtr deviceInfoSet = SetupDiGetClassDevs(
                ref hidGuid, IntPtr.Zero, IntPtr.Zero,
                DIGCF_PRESENT | DIGCF_DEVICEINTERFACE);

            if (deviceInfoSet == IntPtr.Zero || deviceInfoSet == new IntPtr(-1))
            {
                DebugLog("ERROR: SetupDiGetClassDevs failed, error: " + Marshal.GetLastWin32Error());
                return;
            }

            try
            {
                var interfaceData = new SP_DEVICE_INTERFACE_DATA();
                interfaceData.cbSize = (uint)Marshal.SizeOf(interfaceData);

                uint memberIndex = 0;
                int deviceCount = 0;
                
                while (SetupDiEnumDeviceInterfaces(deviceInfoSet, IntPtr.Zero, ref hidGuid, memberIndex, ref interfaceData))
                {
                    memberIndex++;
                    deviceCount++;

                    SetupDiGetDeviceInterfaceDetail(deviceInfoSet, ref interfaceData, IntPtr.Zero, 0, out uint requiredSize, IntPtr.Zero);

                    IntPtr detailDataBuffer = Marshal.AllocHGlobal((int)requiredSize);
                    try
                    {
                        Marshal.WriteInt32(detailDataBuffer, IntPtr.Size == 8 ? 8 : 6);

                        if (SetupDiGetDeviceInterfaceDetail(deviceInfoSet, ref interfaceData, detailDataBuffer, requiredSize, out _, IntPtr.Zero))
                        {
                            string devicePath = Marshal.PtrToStringAuto(detailDataBuffer + 4);
                            
                            // Get device attributes
                            using (var handle = CreateFile(devicePath, 0, FILE_SHARE_READ | FILE_SHARE_WRITE,
                                IntPtr.Zero, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, IntPtr.Zero))
                            {
                                if (!handle.IsInvalid)
                                {
                                    var attributes = new HIDD_ATTRIBUTES();
                                    attributes.Size = (uint)Marshal.SizeOf(attributes);

                                    if (HidD_GetAttributes(handle, ref attributes))
                                    {
                                        DebugLog("Device #{0}: VID=0x{1:X4}, PID=0x{2:X4}, Ver={3}", 
                                            deviceCount, attributes.VendorID, attributes.ProductID, attributes.VersionNumber);
                                        
                                        // Try to get product string
                                        byte[] productBuffer = new byte[256];
                                        if (HidD_GetProductString(handle, productBuffer, 256))
                                        {
                                            string productName = Encoding.Unicode.GetString(productBuffer).TrimEnd('\0');
                                            if (!string.IsNullOrEmpty(productName))
                                                DebugLog("         Product: " + productName);
                                        }
                                        
                                        // Try to get manufacturer string
                                        byte[] mfgBuffer = new byte[256];
                                        if (HidD_GetManufacturerString(handle, mfgBuffer, 256))
                                        {
                                            string mfgName = Encoding.Unicode.GetString(mfgBuffer).TrimEnd('\0');
                                            if (!string.IsNullOrEmpty(mfgName))
                                                DebugLog("         Manufacturer: " + mfgName);
                                        }
                                        
                                        DebugLog("         Path: " + devicePath);
                                    }
                                }
                                else
                                {
                                    DebugLog("Device #{0}: Could not open, error: {1}", deviceCount, Marshal.GetLastWin32Error());
                                    DebugLog("         Path: " + devicePath);
                                }
                            }
                        }
                    }
                    finally
                    {
                        Marshal.FreeHGlobal(detailDataBuffer);
                    }
                }
                
                DebugLog("Total HID devices found: " + deviceCount);
            }
            finally
            {
                SetupDiDestroyDeviceInfoList(deviceInfoSet);
            }
            
            DebugLog("=== End of HID device list ===");
        }

        private List<string> EnumerateHidDevices()
        {
            var devicePaths = new List<string>();
            
            DebugLog("EnumerateHidDevices() called");
            DebugLog("Looking for devices with filters:");
            foreach (var filter in _usbFilters)
            {
                DebugLog("  VID=0x{0:X4}, PID=0x{1:X4}", filter.VendorID, filter.ProductID);
            }
            
            HidD_GetHidGuid(out Guid hidGuid);
            
            IntPtr deviceInfoSet = SetupDiGetClassDevs(
                ref hidGuid, IntPtr.Zero, IntPtr.Zero,
                DIGCF_PRESENT | DIGCF_DEVICEINTERFACE);

            if (deviceInfoSet == IntPtr.Zero || deviceInfoSet == new IntPtr(-1))
            {
                DebugLog("ERROR: SetupDiGetClassDevs failed");
                return devicePaths;
            }

            try
            {
                var interfaceData = new SP_DEVICE_INTERFACE_DATA();
                interfaceData.cbSize = (uint)Marshal.SizeOf(interfaceData);

                uint memberIndex = 0;
                while (SetupDiEnumDeviceInterfaces(deviceInfoSet, IntPtr.Zero, ref hidGuid, memberIndex, ref interfaceData))
                {
                    memberIndex++;

                    SetupDiGetDeviceInterfaceDetail(deviceInfoSet, ref interfaceData, IntPtr.Zero, 0, out uint requiredSize, IntPtr.Zero);

                    IntPtr detailDataBuffer = Marshal.AllocHGlobal((int)requiredSize);
                    try
                    {
                        Marshal.WriteInt32(detailDataBuffer, IntPtr.Size == 8 ? 8 : 6);

                        if (SetupDiGetDeviceInterfaceDetail(deviceInfoSet, ref interfaceData, detailDataBuffer, requiredSize, out _, IntPtr.Zero))
                        {
                            string devicePath = Marshal.PtrToStringAuto(detailDataBuffer + 4);
                            
                            if (CheckDeviceFilter(devicePath))
                            {
                                DebugLog("MATCHED device: " + devicePath);
                                devicePaths.Add(devicePath);
                            }
                        }
                    }
                    finally
                    {
                        Marshal.FreeHGlobal(detailDataBuffer);
                    }
                }
            }
            finally
            {
                SetupDiDestroyDeviceInfoList(deviceInfoSet);
            }

            DebugLog("Found {0} matching devices", devicePaths.Count);
            return devicePaths;
        }

        private bool CheckDeviceFilter(string devicePath)
        {
            using (var handle = CreateFile(devicePath, 0, FILE_SHARE_READ | FILE_SHARE_WRITE,
                IntPtr.Zero, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, IntPtr.Zero))
            {
                if (handle.IsInvalid)
                    return false;

                var attributes = new HIDD_ATTRIBUTES();
                attributes.Size = (uint)Marshal.SizeOf(attributes);

                if (!HidD_GetAttributes(handle, ref attributes))
                    return false;

                foreach (var filter in _usbFilters)
                {
                    int vid = filter.VendorID >= 0 ? filter.VendorID : attributes.VendorID;
                    int pid = filter.ProductID >= 0 ? filter.ProductID : attributes.ProductID;
                    int ver = filter.VersionNumber >= 0 ? filter.VersionNumber : attributes.VersionNumber;

                    if (attributes.VendorID == vid &&
                        attributes.ProductID == pid &&
                        attributes.VersionNumber == ver)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        #endregion

        #region Device Connection

        private HidContext OpenDevice(string devicePath)
        {
            DebugLog("OpenDevice: " + devicePath);
            
            var ctx = new HidContext
            {
                DevicePath = devicePath,
                ReadTimeout = DEFAULT_TIMEOUT,
                WriteTimeout = DEFAULT_TIMEOUT
            };

            ctx.ReadHandle = CreateFile(devicePath, GENERIC_READ,
                FILE_SHARE_READ | FILE_SHARE_WRITE, IntPtr.Zero,
                OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, IntPtr.Zero);

            if (ctx.ReadHandle.IsInvalid)
            {
                ctx.LastError = Marshal.GetLastWin32Error();
                DebugLog("ERROR: Failed to open read handle, error: " + ctx.LastError);
                ctx.Dispose();
                return null;
            }

            ctx.WriteHandle = CreateFile(devicePath, GENERIC_WRITE,
                FILE_SHARE_READ | FILE_SHARE_WRITE, IntPtr.Zero,
                OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, IntPtr.Zero);

            if (ctx.WriteHandle.IsInvalid)
            {
                ctx.LastError = Marshal.GetLastWin32Error();
                DebugLog("ERROR: Failed to open write handle, error: " + ctx.LastError);
                ctx.Dispose();
                return null;
            }

            DebugLog("Device opened successfully");
            return ctx;
        }

        private bool WriteReport(HidContext ctx, byte[] data)
        {
            if (ctx?.WriteHandle == null || ctx.WriteHandle.IsInvalid)
            {
                DebugLog("ERROR: WriteReport - invalid handle");
                return false;
            }

            byte[] buffer = new byte[HID_REPORT_SIZE];
            Array.Copy(data, 0, buffer, 0, Math.Min(data.Length, HID_REPORT_SIZE));

            bool result = WriteFile(ctx.WriteHandle, buffer, HID_REPORT_SIZE, out uint bytesWritten, IntPtr.Zero);
            
            if (!result)
            {
                ctx.LastError = Marshal.GetLastWin32Error();
                DebugLog("ERROR: WriteFile failed, error: " + ctx.LastError);
            }
            else
            {
                DebugLog("WriteReport: {0} bytes written", bytesWritten);
            }

            return result;
        }

        private bool ReadReport(HidContext ctx, byte[] buffer)
        {
            if (ctx?.ReadHandle == null || ctx.ReadHandle.IsInvalid)
            {
                DebugLog("ERROR: ReadReport - invalid handle");
                return false;
            }

            bool result = ReadFile(ctx.ReadHandle, buffer, HID_REPORT_SIZE, out uint bytesRead, IntPtr.Zero);
            
            if (!result)
            {
                ctx.LastError = Marshal.GetLastWin32Error();
                DebugLog("ERROR: ReadFile failed, error: " + ctx.LastError);
            }
            else
            {
                DebugLog("ReadReport: {0} bytes read", bytesRead);
            }

            return result;
        }

        private bool ConnectDevice(string devicePath)
        {
            DebugLog("ConnectDevice: Attempting connection to " + devicePath);
            
            var ctx = OpenDevice(devicePath);
            if (ctx == null)
                return false;

            // Check if this is a GAMDIAS device (VID=0x1B80) - they don't need handshake
            bool isGamdias = devicePath.ToLower().Contains("vid_1b80");
            
            // Check if this is a HWCX device (VID=0x0145) - they also don't need handshake
            bool isHwcx = devicePath.ToLower().Contains("vid_0145");
            
            if (isGamdias)
            {
                DebugLog("GAMDIAS device detected - skipping handshake");
                
                // Mark as GAMDIAS device
                ctx.IsGamdias = true;
                
                // Find empty slot
                int slot = -1;
                for (int i = 0; i < _maxDevices; i++)
                {
                    if (!_devices.ContainsKey(i))
                    {
                        slot = i;
                        break;
                    }
                }

                if (slot < 0)
                {
                    DebugLog("ERROR: No empty slot available");
                    ctx.Dispose();
                    return false;
                }

                _devices[slot] = ctx;
                DebugLog("GAMDIAS device connected and stored in slot " + slot);
                return true;
            }

            if (isHwcx)
            {
                DebugLog("HWCX device detected - skipping handshake, using standard protocol");
                
                // HWCX uses standard protocol (not GAMDIAS)
                ctx.IsGamdias = false;
                
                // Find empty slot
                int slot = -1;
                for (int i = 0; i < _maxDevices; i++)
                {
                    if (!_devices.ContainsKey(i))
                    {
                        slot = i;
                        break;
                    }
                }

                if (slot < 0)
                {
                    DebugLog("ERROR: No empty slot available");
                    ctx.Dispose();
                    return false;
                }

                _devices[slot] = ctx;
                DebugLog("HWCX device connected and stored in slot " + slot);
                return true;
            }

            // Original handshake for other devices
            DebugLog("Unknown device - attempting handshake");
            
            // Generate handshake
            var random = new Random();
            ushort seed = (ushort)(~((ushort)(DateTimeOffset.UtcNow.ToUnixTimeSeconds() / 2592000)));
            random = new Random(seed);
            seed = (ushort)((random.Next(256)) | (random.Next(256) << 8));

            random = new Random((int)DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            
            byte[] sendBuffer = new byte[HID_REPORT_SIZE];
            for (int i = 0; i < HID_REPORT_SIZE; i++)
            {
                sendBuffer[i] = (byte)random.Next(256);
            }

            // Build handshake packet
            sendBuffer[0] = 0;
            sendBuffer[1] = 1;
            sendBuffer[2] = 0;
            sendBuffer[3] = 0;
            sendBuffer[4] = 0;
            sendBuffer[5] = (byte)(seed >> 8);
            sendBuffer[6] = (byte)(seed & 0xFF);

            ushort checkValue = (ushort)(((sendBuffer[7] + (seed >> 8)) << 8) + sendBuffer[8] + (seed & 0xFF));
            checkValue ^= (ushort)((sendBuffer[9] << 8) + sendBuffer[10]);

            sendBuffer[11] = (byte)(checkValue >> 8);
            sendBuffer[12] = (byte)(checkValue & 0xFF);

            // Calculate checksums
            byte checksum1 = 0;
            for (int i = 0; i < 60; i++)
            {
                checksum1 ^= (byte)(i * sendBuffer[i + 1]);
            }
            sendBuffer[63] = checksum1;

            byte checksum2 = 0;
            for (int i = 0; i < 63; i++)
            {
                checksum2 += (byte)(i * sendBuffer[i + 1]);
            }
            sendBuffer[64] = checksum2;

            DebugLog("Sending handshake packet...");
            
            // Send handshake
            if (!WriteReport(ctx, sendBuffer))
            {
                DebugLog("ERROR: Failed to send handshake");
                ctx.Dispose();
                return false;
            }

            DebugLog("Waiting for handshake response...");
            
            // Receive response
            byte[] recvBuffer = new byte[HID_REPORT_SIZE];
            if (!ReadReport(ctx, recvBuffer))
            {
                DebugLog("ERROR: Failed to receive handshake response");
                ctx.Dispose();
                return false;
            }

            // Log received data
            DebugLog("Received response, first 16 bytes: " + BitConverter.ToString(recvBuffer, 0, 16));

            // Verify response checksums
            checksum1 = 0;
            for (int i = 0; i < 60; i++)
            {
                checksum1 ^= (byte)(i * recvBuffer[i + 1]);
            }
            if (recvBuffer[63] != checksum1)
            {
                DebugLog("ERROR: Checksum1 mismatch. Expected: {0}, Got: {1}", checksum1, recvBuffer[63]);
                ctx.Dispose();
                return false;
            }

            checksum2 = 0;
            for (int i = 0; i < 63; i++)
            {
                checksum2 += (byte)(i * recvBuffer[i + 1]);
            }
            if (recvBuffer[64] != checksum2)
            {
                DebugLog("ERROR: Checksum2 mismatch. Expected: {0}, Got: {1}", checksum2, recvBuffer[64]);
                ctx.Dispose();
                return false;
            }

            DebugLog("Handshake successful!");

            // Find empty slot
            int slotIndex = -1;
            for (int i = 0; i < _maxDevices; i++)
            {
                if (!_devices.ContainsKey(i))
                {
                    slotIndex = i;
                    break;
                }
            }

            if (slotIndex < 0)
            {
                DebugLog("ERROR: No empty slot available");
                ctx.Dispose();
                return false;
            }

            _devices[slotIndex] = ctx;
            DebugLog("Device connected and stored in slot " + slotIndex);
            return true;
        }

        #endregion

        #region Public API - Device Management

        /// <summary>
        /// Configure USB VID/PID filters for device detection
        /// </summary>
        public bool SetCheckUsb(byte[] data, int size, int maxDevices)
        {
            lock (_lock)
            {
                DebugLog("SetCheckUsb called: size={0}, maxDevices={1}", size, maxDevices);
                
                if (maxDevices > 20)
                    maxDevices = 20;

                _maxDevices = maxDevices;
                _usbFilters.Clear();

                if (size < 4)
                {
                    DebugLog("WARNING: size < 4, no filters set");
                    return true;
                }

                if (size > 40)
                    size = 40;

                // Parse USB filter data (VID/PID pairs)
                for (int offset = 0; offset + 4 <= size; offset += 4)
                {
                    int vid = (data[offset] << 8) | data[offset + 1];
                    int pid = (data[offset + 2] << 8) | data[offset + 3];
                    
                    DebugLog("Adding filter: VID=0x{0:X4}, PID=0x{1:X4}", vid, pid);
                    
                    _usbFilters.Add(new UsbFilter
                    {
                        VendorID = vid,
                        ProductID = pid
                    });
                }

                // List all HID devices for diagnostics
                ListAllHidDevices();

                return true;
            }
        }

        /// <summary>
        /// Scan for and connect to HID devices
        /// </summary>
        public bool CheckOnlineDevice()
        {
            lock (_lock)
            {
                DebugLog("=== CheckOnlineDevice ===");

                var foundDevices = EnumerateHidDevices();
                var existingPaths = new HashSet<string>();

                // Check which existing devices are still connected
                var toRemove = new List<int>();
                foreach (var kvp in _devices)
                {
                    if (!foundDevices.Contains(kvp.Value.DevicePath))
                    {
                        DebugLog("Device disconnected: " + kvp.Value.DevicePath);
                        kvp.Value.Dispose();
                        toRemove.Add(kvp.Key);
                    }
                    else
                    {
                        existingPaths.Add(kvp.Value.DevicePath);
                    }
                }

                foreach (var key in toRemove)
                {
                    _devices.Remove(key);
                }

                // Connect new devices
                foreach (var devicePath in foundDevices)
                {
                    if (!existingPaths.Contains(devicePath))
                    {
                        DebugLog("Attempting to connect new device: " + devicePath);
                        if (ConnectDevice(devicePath))
                        {
                            DebugLog("Successfully connected: " + devicePath);
                        }
                        else
                        {
                            DebugLog("Failed to connect: " + devicePath);
                        }
                    }
                }

                DebugLog("Connected devices count: " + _devices.Count);
                return true;
            }
        }

        /// <summary>
        /// Get count of connected devices
        /// </summary>
        public int GetOnlineDeviceQty()
        {
            lock (_lock)
            {
                return _devices.Count;
            }
        }

        /// <summary>
        /// Open and connect to devices
        /// </summary>
        public int DeviceOpen()
        {
            DebugLog("=== DeviceOpen ===");
            CheckOnlineDevice();
            int count = _devices.Count;
            DebugLog("DeviceOpen returning: " + count + " devices connected");
            return count > 0 ? 1 : 0;
        }

        /// <summary>
        /// Get last error code
        /// </summary>
        public int GetErrorCode()
        {
            return 0;
        }

        #endregion

        #region Public API - CPU

        public int SetCpuVoltage(byte[] data)
        {
            _cpuVoltage = (ushort)((data[1] << 8) | data[2]);
            return 1;
        }

        public int SetCpuWatt(byte[] data)
        {
            _cpuWatt = (ushort)((data[1] << 8) | data[2]);
            return 1;
        }

        public int SetCpuDynamicInfo(byte[] data)
        {
            // data[0] = load/usage, data[1] = temperature
            _cpuUsage = data[0];
            _cpuTemp = data[1];
            _cpuFrequency = (ushort)(data[2] | (data[3] << 8));
            return 1;
        }

        public int SetFanRPM(short rpm)
        {
            _cpuFanRpm = (ushort)rpm;
            return 1;
        }

        public int SetCpuName() => 1;

        #endregion

        #region Public API - GPU

        public int SetGpuVoltage(byte[] data)
        {
            _gpuVoltage = (ushort)((data[1] << 8) | data[2]);
            return 1;
        }

        public int SetGpuWatt(byte[] data)
        {
            _gpuWatt = (ushort)((data[1] << 8) | data[2]);
            return 1;
        }

        public int SetGpuDynamicInfo(byte[] data)
        {
            // data[0] = load/usage, data[1] = temperature
            _gpuUsage = data[0];
            _gpuTemp = data[1];
            _gpuFrequency = (ushort)(data[2] | (data[3] << 8));
            return 1;
        }

        public int SetGpuFanRPM(short rpm)
        {
            _gpuFanRpm = (ushort)rpm;
            return 1;
        }

        public int SetGpuName() => 1;

        #endregion

        #region Public API - Memory

        public int SetMemDynamicInfo(byte[] data)
        {
            // data[0] = usage, data[1] = temp (if available)
            _memUsage = data[0];
            _memTemp = data[1];
            _memFrequency = (ushort)(data[2] | (data[3] << 8));
            return 1;
        }

        public int SetMemName() => 1;

        #endregion

        #region Public API - Disk

        public int SetDiskDynamicInfo(byte[] data)
        {
            // data[0] = usage/load, data[1] = temp (if available)
            _diskUsage = data[0];
            _diskTemp = data[1];
            return 1;
        }

        public int SetDiskName() => 1;

        #endregion

        #region Public API - Network

        public int SetNetSpeed(int upload, int download)
        {
            _netUpload = upload;
            _netDownload = download;
            return 1;
        }

        #endregion

        #region Public API - Display

        public int SetDisplayMode(int mode, byte flags)
        {
            if (mode == 0)
                flags |= 0x80;
            _displayMode = flags;
            return 1;
        }

        public int SetDisplayName() => 1;

        #endregion

        #region Public API - System

        public int SetOsInfo() => 1;
        public int SetMbName() => 1;
        public int SetMbDynamicInfo() => 1;

        /// <summary>
        /// Send all collected data to connected devices
        /// </summary>
        public int SetDeviceTime(byte[] data)
        {
            // Parse time data
            ushort year = (ushort)((data[0] << 8) | data[1]);
            byte month = data[2];
            byte day = data[3];
            byte dayOfWeek = data[4];
            byte hour = data[5];
            byte minute = data[6];
            byte second = data[7];
            byte extra = data[8];

            lock (_lock)
            {
                if (_devices.Count == 0)
                {
                    DebugLog("SetDeviceTime: No devices connected!");
                    return 0;
                }
                
                var toRemove = new List<int>();

                foreach (var kvp in _devices)
                {
                    byte[] packet;
                    
                    if (kvp.Value.IsGamdias)
                    {
                        // GAMDIAS ATLAS Display packet format
                        packet = BuildGamdiasPacket(hour, minute, second);
                        
                        // Initialize display if not done yet
                        if (!kvp.Value.IsInitialized)
                        {
                            DebugLog("Sending GAMDIAS initialization sequence...");
                            SendGamdiasInit(kvp.Value);
                            kvp.Value.IsInitialized = true;
                        }
                    }
                    else
                    {
                        // Original packet format for other devices
                        packet = BuildStandardPacket(year, month, day, dayOfWeek, hour, minute, second, extra);
                    }
                    
                    int retryCount = 0;
                    bool success = false;

                    while (!success && retryCount < 15)
                    {
                        if (WriteReport(kvp.Value, packet))
                        {
                            success = true;
                            break;
                        }

                        if (kvp.Value.LastError != 995) // ERROR_OPERATION_ABORTED
                        {
                            DebugLog("Send failed, error: {0}, retry: {1}", kvp.Value.LastError, retryCount);

                            // Try to reconnect
                            kvp.Value.Dispose();
                            var newCtx = OpenDevice(kvp.Value.DevicePath);
                            if (newCtx == null)
                            {
                                toRemove.Add(kvp.Key);
                                break;
                            }
                            _devices[kvp.Key] = newCtx;
                        }

                        Thread.Sleep(20);
                        retryCount++;
                    }
                    
                    if (!success)
                    {
                        DebugLog("Failed to send data after retries");
                    }
                }

                foreach (var key in toRemove)
                {
                    _devices.Remove(key);
                }
            }

            return 1;
        }

        /// <summary>
        /// Build packet for GAMDIAS ATLAS display
        /// </summary>
        private byte[] BuildGamdiasPacket(byte hour, byte minute, byte second)
        {
            byte[] packet = new byte[HID_REPORT_SIZE];
            
            // GAMDIAS ATLAS protocol
            // Based on device response: 00-38-B5-01-01-02-00-01-02-07-02-01-FE
            // The device uses little-endian and expects specific header
            
            packet[0] = 0x00;  // Report ID
            packet[1] = 0x38;  // PID low byte (matches device response)
            packet[2] = 0xB5;  // PID high byte
            packet[3] = 0x02;  // Command: data update
            
            // CPU data - FIXED: Display expects temp first, then usage
            packet[4] = (byte)_cpuUsage;          // CPU Usage % (display reads this as temp position)
            packet[5] = (byte)_cpuTemp;           // CPU Temperature (display reads this as usage position)
            packet[6] = (byte)(_cpuFrequency & 0xFF);       // CPU Freq low
            packet[7] = (byte)(_cpuFrequency >> 8);         // CPU Freq high
            
            // GPU data - FIXED: Same swap
            packet[8] = (byte)_gpuUsage;          // GPU Usage %
            packet[9] = (byte)_gpuTemp;           // GPU Temperature
            packet[10] = (byte)(_gpuFrequency & 0xFF);      // GPU Freq low
            packet[11] = (byte)(_gpuFrequency >> 8);        // GPU Freq high
            
            // Fan RPM (little-endian)
            packet[12] = (byte)(_cpuFanRpm & 0xFF);
            packet[13] = (byte)(_cpuFanRpm >> 8);
            packet[14] = (byte)(_gpuFanRpm & 0xFF);
            packet[15] = (byte)(_gpuFanRpm >> 8);
            
            // Memory
            packet[16] = (byte)_memUsage;         // Memory Usage %
            packet[17] = (byte)_memTemp;          // Memory Temp
            
            // Disk
            packet[18] = (byte)_diskUsage;        // Disk Usage %
            packet[19] = (byte)_diskTemp;         // Disk Temp
            
            // Time
            packet[20] = hour;
            packet[21] = minute;
            packet[22] = second;
            
            // Power (little-endian)
            packet[23] = (byte)(_cpuWatt & 0xFF);
            packet[24] = (byte)(_cpuWatt >> 8);
            packet[25] = (byte)(_gpuWatt & 0xFF);
            packet[26] = (byte)(_gpuWatt >> 8);
            
            // Display mode / flags
            packet[27] = _displayMode;
            packet[28] = 0x01;  // Display enable flag

            DebugLog("GAMDIAS packet: CPU temp={0}°C usage={1}%, GPU temp={2}°C usage={3}%", 
                _cpuTemp, _cpuUsage, _gpuTemp, _gpuUsage);
            
            return packet;
        }

        /// <summary>
        /// Send initialization sequence to GAMDIAS display
        /// </summary>
        private void SendGamdiasInit(HidContext ctx)
        {
            // Based on device response pattern: 00-38-B5-01-01-02-00-01-02-07-02-01-FE
            // Try init commands that match the device's own response format
            byte[][] initCommands = new byte[][]
            {
                // Init command 1: Echo device ID (may wake up display)
                new byte[] { 0x00, 0x38, 0xB5, 0x01, 0x00 },
                // Init command 2: Enable display
                new byte[] { 0x00, 0x38, 0xB5, 0x01, 0x01 },
                // Init command 3: Set display mode
                new byte[] { 0x00, 0x38, 0xB5, 0x03, 0x01 },
            };

            foreach (var cmd in initCommands)
            {
                byte[] packet = new byte[HID_REPORT_SIZE];
                Array.Copy(cmd, packet, cmd.Length);
                
                DebugLog("Sending GAMDIAS init: " + BitConverter.ToString(packet, 0, 8));
                WriteReport(ctx, packet);
                Thread.Sleep(100);
            }
            
            DebugLog("GAMDIAS initialization complete");
        }

        /// <summary>
        /// Build standard packet for non-GAMDIAS devices (HWCX)
        /// </summary>
        private byte[] BuildStandardPacket(ushort year, byte month, byte day, byte dayOfWeek, 
            byte hour, byte minute, byte second, byte extra)
        {
            byte[] packet = new byte[HID_REPORT_SIZE];
            packet[0] = 0;
            packet[1] = 2;  // Command: update data
            packet[2] = (byte)_cpuTemp;     // FIXED: Temp first (was usage)
            packet[3] = (byte)_gpuTemp;     // FIXED: Temp first (was usage)
            packet[4] = hour;
            packet[5] = minute;
            packet[6] = second;
            packet[7] = extra;
            packet[8] = (byte)(year >> 8);
            packet[9] = (byte)(year & 0xFF);
            packet[10] = month;
            packet[11] = day;
            packet[12] = dayOfWeek;
            packet[13] = (byte)_cpuUsage;   // FIXED: Usage second (was temp)
            packet[14] = (byte)(_cpuFrequency >> 8);
            packet[15] = (byte)(_cpuFrequency & 0xFF);
            packet[16] = (byte)_gpuUsage;   // FIXED: Usage second (was temp)
            packet[17] = (byte)(_gpuFrequency >> 8);
            packet[18] = (byte)(_gpuFrequency & 0xFF);
            packet[19] = (byte)_memUsage;
            packet[20] = (byte)_memTemp;
            packet[21] = (byte)(_memFrequency >> 8);
            packet[22] = (byte)(_memFrequency & 0xFF);
            packet[23] = (byte)_diskUsage;
            packet[24] = (byte)_diskTemp;
            packet[25] = (byte)(_cpuFanRpm >> 8);
            packet[26] = (byte)(_cpuFanRpm & 0xFF);
            packet[27] = (byte)(_cpuVoltage >> 8);
            packet[28] = (byte)(_cpuVoltage & 0xFF);
            packet[29] = (byte)(_cpuWatt >> 8);
            packet[30] = (byte)(_cpuWatt & 0xFF);
            packet[31] = (byte)(_gpuFanRpm >> 8);
            packet[32] = (byte)(_gpuFanRpm & 0xFF);
            packet[33] = (byte)(_gpuVoltage >> 8);
            packet[34] = (byte)(_gpuVoltage & 0xFF);
            packet[35] = (byte)(_gpuWatt >> 8);
            packet[36] = (byte)(_gpuWatt & 0xFF);
            packet[37] = _reserved1;
            packet[38] = _reserved2;
            packet[39] = _reserved3;
            packet[40] = _reserved4;
            packet[41] = (byte)(_netUpload >> 24);
            packet[42] = (byte)(_netUpload >> 16);
            packet[43] = (byte)(_netUpload >> 8);
            packet[44] = (byte)_netUpload;
            packet[45] = (byte)(_netDownload >> 24);
            packet[46] = (byte)(_netDownload >> 16);
            packet[47] = (byte)(_netDownload >> 8);
            packet[48] = (byte)_netDownload;
            packet[49] = _displayMode;
            
            DebugLog("HWCX packet: CPU temp={0}°C usage={1}%, GPU temp={2}°C usage={3}%", 
                _cpuTemp, _cpuUsage, _gpuTemp, _gpuUsage);
            
            return packet;
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                lock (_lock)
                {
                    foreach (var kvp in _devices)
                    {
                        kvp.Value.Dispose();
                    }
                    _devices.Clear();
                }

                DebugLog("=== HidDevice Library Disposed ===");
            }

            _disposed = true;
        }

        ~HidDevice()
        {
            Dispose(false);
        }

        #endregion
    }
}
