using System;
using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;
using System.Text;
using System.Collections.Generic;

namespace HardwareHelp
{
    /// <summary>
    /// Reads sensor data from HWiNFO shared memory
    /// HWiNFO must be running with "Shared Memory Support" enabled in settings
    /// </summary>
    public class HWiNFOReader : IDisposable
    {
        private const string HWINFO_SHARED_MEM_FILE_NAME = "Global\\HWiNFO_SENS_SM2";
        private const int HWINFO_SENSORS_STRING_LEN = 128;
        private const int HWINFO_UNIT_STRING_LEN = 16;

        private MemoryMappedFile _mmf;
        private MemoryMappedViewAccessor _accessor;
        private bool _isAvailable;

        #region Structures

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct HWiNFOSharedMemHeader
        {
            public uint Signature;           // "HWiS" = 0x53695748
            public uint Version;             // Version of shared memory
            public uint Revision;            // Revision
            public long PollTime;            // Polling time in ms
            public uint OffsetOfSensorSection;
            public uint SizeOfSensorElement;
            public uint NumSensorElements;
            public uint OffsetOfReadingSection;
            public uint SizeOfReadingElement;
            public uint NumReadingElements;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct HWiNFOSensorElement
        {
            public uint SensorId;
            public uint SensorInstance;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = HWINFO_SENSORS_STRING_LEN)]
            public byte[] SensorNameOrig;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = HWINFO_SENSORS_STRING_LEN)]
            public byte[] SensorNameUser;

            public string GetSensorName()
            {
                string userStr = Encoding.ASCII.GetString(SensorNameUser).TrimEnd('\0');
                if (!string.IsNullOrEmpty(userStr))
                    return userStr;
                return Encoding.ASCII.GetString(SensorNameOrig).TrimEnd('\0');
            }
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct HWiNFOReadingElement
        {
            public uint ReadingType;         // 0=None, 1=Temp, 2=Voltage, 3=Fan, 4=Current, 5=Power, 6=Clock, 7=Usage, 8=Other
            public uint SensorIndex;
            public uint ReadingId;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = HWINFO_SENSORS_STRING_LEN)]
            public byte[] LabelOrig;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = HWINFO_SENSORS_STRING_LEN)]
            public byte[] LabelUser;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = HWINFO_UNIT_STRING_LEN)]
            public byte[] Unit;
            public double Value;
            public double ValueMin;
            public double ValueMax;
            public double ValueAvg;

            public string GetLabel()
            {
                string userStr = Encoding.ASCII.GetString(LabelUser).TrimEnd('\0');
                if (!string.IsNullOrEmpty(userStr))
                    return userStr;
                return Encoding.ASCII.GetString(LabelOrig).TrimEnd('\0');
            }

            public string GetUnit()
            {
                return Encoding.ASCII.GetString(Unit).TrimEnd('\0');
            }
        }

        public enum ReadingType : uint
        {
            None = 0,
            Temperature = 1,
            Voltage = 2,
            Fan = 3,
            Current = 4,
            Power = 5,
            Clock = 6,
            Usage = 7,
            Other = 8
        }

        #endregion

        #region Public Data Classes

        public class GpuData
        {
            public string Name { get; set; }
            public float Temperature { get; set; }
            public float Usage { get; set; }
            public float MemoryUsage { get; set; }
            public float FanRpm { get; set; }
            public float Power { get; set; }
            public float CoreClock { get; set; }
            public bool IsValid { get; set; }
        }

        #endregion

        public bool IsAvailable => _isAvailable;

        public HWiNFOReader()
        {
            TryConnect();
        }

        public bool TryConnect()
        {
            try
            {
                _mmf = MemoryMappedFile.OpenExisting(HWINFO_SHARED_MEM_FILE_NAME, MemoryMappedFileRights.Read);
                _accessor = _mmf.CreateViewAccessor(0, 0, MemoryMappedFileAccess.Read);
                _isAvailable = true;
                return true;
            }
            catch
            {
                _isAvailable = false;
                return false;
            }
        }

        public GpuData GetGpuData()
        {
            return GetGpuData(null);
        }

        public GpuData GetGpuData(Action<string> logCallback)
        {
            if (!_isAvailable)
            {
                if (!TryConnect())
                    return null;
            }

            try
            {
                // Read header
                var header = ReadStructure<HWiNFOSharedMemHeader>(0);
                
                // Verify signature "HWiS" = 0x53695748
                if (header.Signature != 0x53695748)
                {
                    logCallback?.Invoke($"HWiNFO: Invalid signature 0x{header.Signature:X8}");
                    _isAvailable = false;
                    return null;
                }

                logCallback?.Invoke($"HWiNFO: Found {header.NumSensorElements} sensors, {header.NumReadingElements} readings");

                // Read all sensors to find GPU
                var sensors = new List<HWiNFOSensorElement>();
                int gpuSensorIndex = -1;
                string gpuName = "";

                for (int i = 0; i < header.NumSensorElements; i++)
                {
                    long offset = header.OffsetOfSensorSection + (i * header.SizeOfSensorElement);
                    var sensor = ReadStructure<HWiNFOSensorElement>(offset);
                    sensors.Add(sensor);

                    string sensorName = sensor.GetSensorName();
                    string sensorNameUpper = sensorName.ToUpper();
                    
                    // Log all sensor names for debugging (first time only)
                    logCallback?.Invoke($"HWiNFO sensor[{i}]: {sensorName}");
                    
                    // Look for GPU sensors - check for various patterns
                    bool isGpu = false;
                    
                    // AMD patterns
                    if (sensorNameUpper.Contains("RADEON") || sensorNameUpper.Contains("RX ") || 
                        sensorNameUpper.Contains("RX9") || sensorNameUpper.Contains("NAVI") ||
                        sensorNameUpper.Contains("VEGA") || sensorNameUpper.Contains("RDNA"))
                        isGpu = true;
                    
                    // NVIDIA patterns
                    if (sensorNameUpper.Contains("NVIDIA") || sensorNameUpper.Contains("GEFORCE") ||
                        sensorNameUpper.Contains("RTX") || sensorNameUpper.Contains("GTX"))
                        isGpu = true;
                    
                    // Intel patterns
                    if (sensorNameUpper.Contains("INTEL") && (sensorNameUpper.Contains("UHD") || 
                        sensorNameUpper.Contains("IRIS") || sensorNameUpper.Contains("ARC")))
                        isGpu = true;
                    
                    // Generic GPU pattern - HWiNFO often names GPU sensors as "GPU [#0]" etc
                    if (sensorNameUpper.StartsWith("GPU") || sensorNameUpper.Contains("GPU ["))
                        isGpu = true;
                    
                    // XFX is a GPU brand
                    if (sensorNameUpper.Contains("XFX"))
                        isGpu = true;

                    if (isGpu && gpuSensorIndex < 0)
                    {
                        gpuSensorIndex = i;
                        gpuName = sensorName;
                        logCallback?.Invoke($"HWiNFO: Found GPU at index {i}: {gpuName}");
                    }
                }

                if (gpuSensorIndex < 0)
                {
                    logCallback?.Invoke("HWiNFO: No GPU sensor found in sensor list");
                    return null;
                }

                // Read all readings and find GPU-related ones
                var gpuData = new GpuData
                {
                    Name = gpuName,
                    IsValid = false
                };

                for (int i = 0; i < header.NumReadingElements; i++)
                {
                    long offset = header.OffsetOfReadingSection + (i * header.SizeOfReadingElement);
                    var reading = ReadStructure<HWiNFOReadingElement>(offset);

                    if (reading.SensorIndex != gpuSensorIndex)
                        continue;

                    string label = reading.GetLabel();
                    string labelUpper = label.ToUpper();
                    var readingType = (ReadingType)reading.ReadingType;

                    // Log all readings for this GPU sensor
                    logCallback?.Invoke($"HWiNFO GPU reading: type={readingType}, label={label}, value={reading.Value}");

                    // GPU Temperature - be more flexible
                    if (readingType == ReadingType.Temperature)
                    {
                        // Accept any temperature reading from GPU sensor
                        if (labelUpper.Contains("GPU") || labelUpper.Contains("EDGE") || 
                            labelUpper.Contains("JUNCTION") || labelUpper.Contains("HOTSPOT") || 
                            labelUpper.Contains("CORE") || labelUpper.Contains("TEMP"))
                        {
                            // Prefer edge/core temp over junction/hotspot
                            if (gpuData.Temperature == 0 || labelUpper.Contains("EDGE") || 
                                labelUpper.Contains("CORE") || labelUpper == "GPU TEMPERATURE")
                            {
                                gpuData.Temperature = (float)reading.Value;
                                gpuData.IsValid = true;
                            }
                        }
                        // If no temp yet, just take the first temperature reading
                        else if (gpuData.Temperature == 0)
                        {
                            gpuData.Temperature = (float)reading.Value;
                            gpuData.IsValid = true;
                        }
                    }
                    // GPU Usage - be more strict about what counts as core usage
                    else if (readingType == ReadingType.Usage)
                    {
                        // First priority: specific GPU core/utilization labels
                        if (labelUpper.Contains("GPU CORE LOAD") || labelUpper.Contains("GPU UTILIZATION") ||
                            labelUpper == "GPU LOAD" || labelUpper == "GPU USAGE" ||
                            labelUpper.Contains("GPU CORE UTIL"))
                        {
                            gpuData.Usage = (float)reading.Value;
                            gpuData.IsValid = true;
                            logCallback?.Invoke($"HWiNFO: Using '{label}' as GPU usage: {reading.Value}%");
                        }
                        // Second priority: generic GPU activity (but not memory/VR/video)
                        else if (gpuData.Usage == 0 && labelUpper.Contains("GPU") && 
                                 !labelUpper.Contains("MEMORY") && !labelUpper.Contains("VR") &&
                                 !labelUpper.Contains("VIDEO") && !labelUpper.Contains("D3D") &&
                                 !labelUpper.Contains("CONTROLLER"))
                        {
                            gpuData.Usage = (float)reading.Value;
                            gpuData.IsValid = true;
                            logCallback?.Invoke($"HWiNFO: Using '{label}' as GPU usage (fallback): {reading.Value}%");
                        }
                        // Track memory usage separately
                        if (labelUpper.Contains("MEMORY") && labelUpper.Contains("USAGE"))
                        {
                            gpuData.MemoryUsage = (float)reading.Value;
                        }
                    }
                    // GPU Fan
                    else if (readingType == ReadingType.Fan)
                    {
                        if (gpuData.FanRpm == 0) // Take first fan reading
                        {
                            gpuData.FanRpm = (float)reading.Value;
                        }
                    }
                    // GPU Power
                    else if (readingType == ReadingType.Power)
                    {
                        if (labelUpper.Contains("GPU") || labelUpper.Contains("TOTAL") || 
                            labelUpper.Contains("BOARD") || labelUpper.Contains("TGP") ||
                            labelUpper.Contains("ASIC"))
                        {
                            if (gpuData.Power == 0)
                            {
                                gpuData.Power = (float)reading.Value;
                            }
                        }
                    }
                    // GPU Clock
                    else if (readingType == ReadingType.Clock)
                    {
                        if (labelUpper.Contains("GPU") || labelUpper.Contains("CORE") ||
                            labelUpper.Contains("GFX"))
                        {
                            if (gpuData.CoreClock == 0)
                            {
                                gpuData.CoreClock = (float)reading.Value;
                            }
                        }
                    }
                }

                if (gpuData.IsValid)
                {
                    logCallback?.Invoke($"HWiNFO GPU result: temp={gpuData.Temperature}°C, usage={gpuData.Usage}%, fan={gpuData.FanRpm}RPM");
                }
                else
                {
                    logCallback?.Invoke("HWiNFO: GPU sensor found but no valid readings extracted");
                }

                return gpuData.IsValid ? gpuData : null;
            }
            catch (Exception ex)
            {
                logCallback?.Invoke($"HWiNFO exception: {ex.Message}");
                _isAvailable = false;
                return null;
            }
        }

        private T ReadStructure<T>(long position) where T : struct
        {
            int size = Marshal.SizeOf<T>();
            byte[] buffer = new byte[size];
            _accessor.ReadArray(position, buffer, 0, size);

            GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            try
            {
                return Marshal.PtrToStructure<T>(handle.AddrOfPinnedObject());
            }
            finally
            {
                handle.Free();
            }
        }

        public void Dispose()
        {
            _accessor?.Dispose();
            _mmf?.Dispose();
            _accessor = null;
            _mmf = null;
            _isAvailable = false;
        }
    }
}
