using System;

namespace HidDeviceLib
{
    /// <summary>
    /// Static wrapper class providing the same API as the original DLL
    /// For backward compatibility with existing code
    /// Returns uint to match original DLL interface expectations
    /// </summary>
    public static class HidDeviceApi
    {
        private static readonly Lazy<HidDevice> _instance = new Lazy<HidDevice>(() => new HidDevice());
        
        private static HidDevice Device => _instance.Value;

        // Device Management
        public static bool SetCheckUsb(byte[] data, int size, int maxDevices) 
            => Device.SetCheckUsb(data, size, maxDevices);

        public static bool CheckOnlineDevice() 
            => Device.CheckOnlineDevice();

        public static uint GetOnlineDeviceQty() 
            => (uint)Device.GetOnlineDeviceQty();

        public static uint DeviceOpen() 
            => (uint)Device.DeviceOpen();

        public static uint GetErrorCode() 
            => (uint)Device.GetErrorCode();

        // CPU
        public static uint SetCpuVoltage(byte[] data) 
            => (uint)Device.SetCpuVoltage(data);

        public static uint SetCpuWatt(byte[] data) 
            => (uint)Device.SetCpuWatt(data);

        public static uint SetCpuDynamicInfo(byte[] data) 
            => (uint)Device.SetCpuDynamicInfo(data);

        public static uint SetFanRPM(short rpm) 
            => (uint)Device.SetFanRPM(rpm);

        public static uint SetCpuName() 
            => (uint)Device.SetCpuName();

        // GPU
        public static uint SetGpuVoltage(byte[] data) 
            => (uint)Device.SetGpuVoltage(data);

        public static uint SetGpuWatt(byte[] data) 
            => (uint)Device.SetGpuWatt(data);

        public static uint SetGpuDynamicInfo(byte[] data) 
            => (uint)Device.SetGpuDynamicInfo(data);

        public static uint SetGpuFanRPM(short rpm) 
            => (uint)Device.SetGpuFanRPM(rpm);

        public static uint SetGpuName() 
            => (uint)Device.SetGpuName();

        // Memory
        public static uint SetMemDynamicInfo(byte[] data) 
            => (uint)Device.SetMemDynamicInfo(data);

        public static uint SetMemName() 
            => (uint)Device.SetMemName();

        // Disk
        public static uint SetDiskDynamicInfo(byte[] data) 
            => (uint)Device.SetDiskDynamicInfo(data);

        public static uint SetDiskName() 
            => (uint)Device.SetDiskName();

        // Network
        public static uint SetNetSpeed(int upload, int download) 
            => (uint)Device.SetNetSpeed(upload, download);

        // Display
        public static uint SetDisplayMode(int mode, byte flags) 
            => (uint)Device.SetDisplayMode(mode, flags);

        public static uint SetDisplayName() 
            => (uint)Device.SetDisplayName();

        // System
        public static uint SetOsInfo() 
            => (uint)Device.SetOsInfo();

        public static uint SetMbName() 
            => (uint)Device.SetMbName();

        public static uint SetMbDynamicInfo() 
            => (uint)Device.SetMbDynamicInfo();

        public static uint SetDeviceTime(byte[] data) 
            => (uint)Device.SetDeviceTime(data);

        /// <summary>
        /// Dispose the singleton instance (call on application exit)
        /// </summary>
        public static void Shutdown()
        {
            if (_instance.IsValueCreated)
            {
                _instance.Value.Dispose();
            }
        }
    }
}
