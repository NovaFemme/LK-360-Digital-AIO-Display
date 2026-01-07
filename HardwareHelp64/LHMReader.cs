using System;
using System.Collections.Generic;
using System.Linq;

// LibreHardwareMonitor integration
// Requires NuGet package: LibreHardwareMonitorLib
// Install-Package LibreHardwareMonitorLib

namespace HardwareHelp
{
    /// <summary>
    /// Reads hardware sensor data using LibreHardwareMonitor library
    /// This provides built-in GPU monitoring without external applications
    /// </summary>
    public class LHMReader : IDisposable
    {
        private LibreHardwareMonitor.Hardware.Computer _computer;
        private bool _isInitialized;
        private bool _disposed;

        #region Data Classes

        public class GpuData
        {
            public string Name { get; set; }
            public float Temperature { get; set; }
            public float Usage { get; set; }
            public float MemoryUsage { get; set; }
            public float FanRpm { get; set; }
            public float FanPercent { get; set; }
            public float Power { get; set; }
            public float CoreClock { get; set; }
            public float MemoryClock { get; set; }
            public bool IsValid { get; set; }
            public bool HasTemperature { get; set; }  // True if we have actual temp sensor
        }

        public class CpuData
        {
            public string Name { get; set; }
            public float Temperature { get; set; }
            public float Usage { get; set; }
            public float Power { get; set; }
            public float CoreClock { get; set; }
            public bool IsValid { get; set; }
        }

        #endregion

        public bool IsAvailable => _isInitialized && _computer != null;

        public LHMReader()
        {
            TryInitialize();
        }

        public bool TryInitialize()
        {
            if (_isInitialized)
                return true;

            try
            {
                _computer = new LibreHardwareMonitor.Hardware.Computer
                {
                    IsCpuEnabled = true,
                    IsGpuEnabled = true,
                    IsMemoryEnabled = true,
                    IsMotherboardEnabled = false,
                    IsControllerEnabled = false,
                    IsNetworkEnabled = false,
                    IsStorageEnabled = false
                };

                _computer.Open();
                _isInitialized = true;
                return true;
            }
            catch (Exception)
            {
                _isInitialized = false;
                return false;
            }
        }

        /// <summary>
        /// Update all sensor readings - call this before reading data
        /// </summary>
        public void Update()
        {
            if (!_isInitialized || _computer == null)
                return;

            try
            {
                foreach (var hardware in _computer.Hardware)
                {
                    hardware.Update();
                    foreach (var subHardware in hardware.SubHardware)
                    {
                        subHardware.Update();
                    }
                }
            }
            catch
            {
                // Ignore update errors
            }
        }

        /// <summary>
        /// Get GPU sensor data
        /// </summary>
        public GpuData GetGpuData(Action<string> logCallback = null)
        {
            if (!_isInitialized || _computer == null)
            {
                logCallback?.Invoke("LHM: Not initialized");
                return null;
            }

            try
            {
                Update();

                // Find GPU hardware
                var gpuHardware = _computer.Hardware.FirstOrDefault(h =>
                    h.HardwareType == LibreHardwareMonitor.Hardware.HardwareType.GpuAmd ||
                    h.HardwareType == LibreHardwareMonitor.Hardware.HardwareType.GpuNvidia ||
                    h.HardwareType == LibreHardwareMonitor.Hardware.HardwareType.GpuIntel);

                if (gpuHardware == null)
                {
                    logCallback?.Invoke("LHM: No GPU hardware found");
                    
                    // List all hardware for debugging
                    foreach (var hw in _computer.Hardware)
                    {
                        logCallback?.Invoke($"LHM hardware: {hw.Name} ({hw.HardwareType})");
                    }
                    return null;
                }

                logCallback?.Invoke($"LHM: Found GPU: {gpuHardware.Name} ({gpuHardware.HardwareType})");

                var gpuData = new GpuData
                {
                    Name = gpuHardware.Name,
                    IsValid = false
                };

                // Read all sensors
                foreach (var sensor in gpuHardware.Sensors)
                {
                    if (!sensor.Value.HasValue)
                        continue;

                    float value = sensor.Value.Value;
                    string name = sensor.Name.ToUpper();

                    logCallback?.Invoke($"LHM GPU sensor: {sensor.Name} = {value} ({sensor.SensorType})");

                    switch (sensor.SensorType)
                    {
                        case LibreHardwareMonitor.Hardware.SensorType.Temperature:
                            // Prefer "GPU Core" or "GPU Temperature" over hotspot
                            if (name.Contains("CORE") || name.Contains("GPU") && !name.Contains("HOT"))
                            {
                                if (gpuData.Temperature == 0 || name.Contains("CORE"))
                                {
                                    gpuData.Temperature = value;
                                    gpuData.IsValid = true;
                                    gpuData.HasTemperature = true;
                                }
                            }
                            else if (gpuData.Temperature == 0)
                            {
                                gpuData.Temperature = value;
                                gpuData.IsValid = true;
                                gpuData.HasTemperature = true;
                            }
                            break;

                        case LibreHardwareMonitor.Hardware.SensorType.Load:
                            // Check for core/GPU usage first
                            if (name.Contains("CORE") || (name.Contains("GPU") && !name.Contains("MEMORY")))
                            {
                                if (gpuData.Usage == 0)
                                {
                                    gpuData.Usage = value;
                                    gpuData.IsValid = true;
                                }
                            }
                            // D3D 3D is the main GPU rendering load - use as fallback
                            else if (name.Contains("D3D 3D") || name == "D3D 3D")
                            {
                                if (gpuData.Usage == 0)
                                {
                                    gpuData.Usage = value;
                                    gpuData.IsValid = true;
                                    logCallback?.Invoke($"LHM: Using D3D 3D load as GPU usage: {value}%");
                                }
                            }
                            else if (name.Contains("MEMORY"))
                            {
                                gpuData.MemoryUsage = value;
                            }
                            break;

                        case LibreHardwareMonitor.Hardware.SensorType.Fan:
                            if (gpuData.FanRpm == 0)
                            {
                                gpuData.FanRpm = value;
                            }
                            break;

                        case LibreHardwareMonitor.Hardware.SensorType.Control:
                            if (name.Contains("FAN") && gpuData.FanPercent == 0)
                            {
                                gpuData.FanPercent = value;
                            }
                            break;

                        case LibreHardwareMonitor.Hardware.SensorType.Power:
                            if (name.Contains("TOTAL") || name.Contains("PACKAGE") || name.Contains("GPU"))
                            {
                                if (gpuData.Power == 0)
                                {
                                    gpuData.Power = value;
                                }
                            }
                            break;

                        case LibreHardwareMonitor.Hardware.SensorType.Clock:
                            if (name.Contains("CORE") || name.Contains("GPU"))
                            {
                                if (gpuData.CoreClock == 0)
                                {
                                    gpuData.CoreClock = value;
                                }
                            }
                            else if (name.Contains("MEMORY"))
                            {
                                gpuData.MemoryClock = value;
                            }
                            break;
                    }
                }

                if (gpuData.IsValid)
                {
                    logCallback?.Invoke($"LHM GPU result: temp={gpuData.Temperature}°C, usage={gpuData.Usage}%, fan={gpuData.FanRpm}RPM");
                }
                else
                {
                    logCallback?.Invoke("LHM: GPU found but no valid temperature or usage sensors available (GPU may be too new for LHM)");
                }

                return gpuData.IsValid ? gpuData : null;
            }
            catch (Exception ex)
            {
                logCallback?.Invoke($"LHM exception: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Get CPU sensor data
        /// </summary>
        public CpuData GetCpuData(Action<string> logCallback = null)
        {
            if (!_isInitialized || _computer == null)
                return null;

            try
            {
                Update();

                var cpuHardware = _computer.Hardware.FirstOrDefault(h =>
                    h.HardwareType == LibreHardwareMonitor.Hardware.HardwareType.Cpu);

                if (cpuHardware == null)
                    return null;

                var cpuData = new CpuData
                {
                    Name = cpuHardware.Name,
                    IsValid = false
                };

                foreach (var sensor in cpuHardware.Sensors)
                {
                    if (!sensor.Value.HasValue)
                        continue;

                    float value = sensor.Value.Value;
                    string name = sensor.Name.ToUpper();

                    switch (sensor.SensorType)
                    {
                        case LibreHardwareMonitor.Hardware.SensorType.Temperature:
                            if (name.Contains("PACKAGE") || name.Contains("CORE") || name.Contains("CPU"))
                            {
                                if (cpuData.Temperature == 0 || name.Contains("PACKAGE"))
                                {
                                    cpuData.Temperature = value;
                                    cpuData.IsValid = true;
                                }
                            }
                            break;

                        case LibreHardwareMonitor.Hardware.SensorType.Load:
                            if (name.Contains("TOTAL") || name.Contains("CPU"))
                            {
                                if (cpuData.Usage == 0 || name.Contains("TOTAL"))
                                {
                                    cpuData.Usage = value;
                                    cpuData.IsValid = true;
                                }
                            }
                            break;

                        case LibreHardwareMonitor.Hardware.SensorType.Power:
                            if (name.Contains("PACKAGE") || name.Contains("CPU"))
                            {
                                if (cpuData.Power == 0)
                                {
                                    cpuData.Power = value;
                                }
                            }
                            break;

                        case LibreHardwareMonitor.Hardware.SensorType.Clock:
                            if (cpuData.CoreClock == 0)
                            {
                                cpuData.CoreClock = value;
                            }
                            break;
                    }
                }

                return cpuData.IsValid ? cpuData : null;
            }
            catch
            {
                return null;
            }
        }

        public void Dispose()
        {
            if (_disposed)
                return;

            _disposed = true;

            try
            {
                _computer?.Close();
            }
            catch
            {
                // Ignore disposal errors
            }

            _computer = null;
            _isInitialized = false;
        }
    }
}
