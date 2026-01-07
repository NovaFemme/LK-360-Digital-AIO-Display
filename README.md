# LK Digital Display

A Windows service application for GAMDIAS ATLAS AIO cooler LCD displays, providing real-time CPU and GPU monitoring.

## Features

- **Real-time CPU Monitoring**
  - Temperature (°C)
  - Usage (%)
  - Core Frequency (GHz)
  
- **Real-time GPU Monitoring**
  - Temperature (°C)
  - Usage (%)
  - Core Frequency (GHz)
  - Fan RPM
  - Power Draw (W)

- **Multi-Source Hardware Monitoring**
  - Primary: CPUID SDK
  - Fallback 1: LibreHardwareMonitor (built-in)
  - Fallback 2: HWiNFO (via shared memory)

- **Broad GPU Compatibility**
  - AMD Radeon (including RX 9000 series RDNA 4)
  - NVIDIA GeForce
  - Intel Arc

## Supported Hardware

| Device | VID | PID | Status |
|--------|-----|-----|--------|
| GAMDIAS ATLAS | 0x1B80 | 0xB538 | ✅ Supported |
| HWCX Controller | 0x0145 | 0x1005 | ✅ Supported |

## Requirements

### Runtime Requirements
- Windows 10/11 (64-bit)
- .NET Framework 4.8
- Administrator privileges (required for hardware access)

### For GPU Monitoring (AMD GPUs)
For newer AMD GPUs (RX 7000/9000 series), one of the following is recommended:
- **HWiNFO** (recommended) - [Download](https://www.hwinfo.com/)
- **LibreHardwareMonitor** - Built-in fallback

## Installation

### Pre-built Release
1. Download the latest release from [Releases](../../releases)
2. Extract to a folder (e.g., `C:\Program Files\LK Digital Display`)
3. Run `LK Digital Display.exe` as Administrator

### Install as Windows Service (Optional)
To run automatically at startup:
```cmd
# Run Command Prompt as Administrator
cd "C:\Program Files\LK Digital Display"
"LK Digital Display.exe" install
net start "LK Digital Display"
```

To uninstall the service:
```cmd
net stop "LK Digital Display"
"LK Digital Display.exe" uninstall
```

## Building from Source

### Prerequisites
- Visual Studio 2019 or later
- .NET Framework 4.8 SDK
- NuGet Package Manager

### Build Steps

1. **Clone the repository**
   ```bash
   git clone https://github.com/yourusername/LK-Digital-Display.git
   cd LK-Digital-Display
   ```

2. **Restore NuGet packages**
   ```bash
   nuget restore LKDigitalDisplay_All.sln
   ```
   Or in Visual Studio: Right-click solution → "Restore NuGet Packages"

3. **Build the solution**
   ```bash
   msbuild LKDigitalDisplay_All.sln /p:Configuration=Release /p:Platform=x64
   ```
   Or in Visual Studio: Build → Build Solution (F6)

4. **Output location**
   ```
   LK Digital Display\bin\x64\Release\
   ```

### Project Structure

```
LKDigitalDisplay_All.sln
├── LK Digital Display/     # Main Windows Service application
├── XKWLib/                  # Core library (hardware monitoring, USB communication)
├── HardwareHelp64/          # Hardware abstraction layer
│   ├── CPUIDSDK.cs         # CPUID SDK integration
│   ├── LHMReader.cs        # LibreHardwareMonitor integration
│   └── HWiNFOReader.cs     # HWiNFO shared memory integration
└── TempComm/               # USB HID communication library
    └── HidDevice.cs        # Device protocol implementation
```

### NuGet Dependencies

| Package | Version | Project |
|---------|---------|---------|
| LibreHardwareMonitorLib | 0.9.3+ | HardwareHelp64 |
| HidSharp | 2.1.0+ | HardwareHelp64 |
| Newtonsoft.Json | 13.0+ | XKWLib, HardwareHelp64 |

## Configuration

Configuration is stored in `config.ini`:

```ini
[config]
fefresh_delay=500      # Display refresh rate (ms)
send_delay=500         # USB send delay (ms)
IsLog=True             # Enable logging
LogPath=               # Log file path (empty = app directory)
localPort=8890         # UDP port for remote monitoring
IsUdp=True             # Enable UDP server
```

## HWiNFO Setup (Recommended for AMD GPUs)

1. Download and install [HWiNFO](https://www.hwinfo.com/)
2. Run HWiNFO in **Sensors-only** mode
3. Go to **Settings** (gear icon)
4. Enable **"Shared Memory Support"**
5. Click OK and keep HWiNFO running

The application will automatically detect and use HWiNFO data when available.

## Hardware Monitoring Fallback Chain

The application uses a cascading fallback system for maximum compatibility:

```
┌─────────────────────────────────────────────────────────────┐
│                    GPU Monitoring                           │
├─────────────────────────────────────────────────────────────┤
│  1. CPUID SDK (Primary)                                     │
│     └─ Works with most NVIDIA/Intel GPUs                    │
│                    ↓ (if no GPU data)                       │
│  2. LibreHardwareMonitor (Built-in Fallback)                │
│     └─ Works with AMD/NVIDIA/Intel                          │
│     └─ May have limited support for newest GPUs             │
│                    ↓ (if no temperature data)               │
│  3. HWiNFO Shared Memory (External Fallback)                │
│     └─ Best support for newest AMD GPUs (RDNA 4)            │
│     └─ Requires HWiNFO running separately                   │
└─────────────────────────────────────────────────────────────┘
```

## Troubleshooting

### Display Not Detected
- Ensure the USB cable is connected to both the AIO pump and motherboard USB header
- Check Device Manager for "HID-compliant device" with VID 0x1B80
- Try a different USB port

### GPU Data Not Showing
1. Check `log.txt` for error messages
2. For AMD RX 7000/9000 series: Install and run HWiNFO with Shared Memory enabled
3. Ensure no other monitoring software is blocking access (e.g., Zeuscast, iCUE)

### CPU Temperature Shows 0°C or Wrong Value
- Run the application as Administrator
- Check if antivirus is blocking hardware access
- Verify CPUID SDK DLLs are present (`cpuidsdk64.dll`)

### Service Won't Start
```cmd
# Check Windows Event Viewer for errors
eventvwr.msc → Windows Logs → Application

# Try running directly first
"LK Digital Display.exe" console
```

### Log File Location
Default: Application directory (`log.txt`)

Enable verbose logging in `config.ini`:
```ini
IsLog=True
```

## Technical Reference

### USB Protocol

- **Interface:** USB HID
- **Packet Size:** 64 bytes
- **Update Rate:** 500ms (configurable)

### Data Packet Structure (GAMDIAS Protocol)

| Byte | Description |
|------|-------------|
| 0 | Report ID (0xB0) |
| 1 | Command (0x01 = Dynamic Data) |
| 2 | Sub-command |
| 3 | GPU Temperature |
| 4-5 | CPU Temperature (16-bit) |
| 6 | CPU Usage % |
| 14-15 | CPU Frequency (MHz, big-endian) |
| 16 | GPU Usage % |
| 17-18 | GPU Frequency (MHz, big-endian) |
| ... | Additional data |

### HWiNFO Shared Memory Integration

The application reads from HWiNFO's shared memory (`Global\HWiNFO_SENS_SM2`) to access sensor data:

```csharp
// Shared memory structure
struct HWiNFOSharedMemHeader {
    uint Signature;           // "HWiS" = 0x53695748
    uint Version;
    uint Revision;
    long PollTime;
    uint OffsetOfSensorSection;
    uint SizeOfSensorElement;
    uint NumSensorElements;
    uint OffsetOfReadingSection;
    uint SizeOfReadingElement;
    uint NumReadingElements;
}
```

### LibreHardwareMonitor Integration

Built-in support via the `LibreHardwareMonitorLib` NuGet package:

```csharp
var computer = new Computer {
    IsCpuEnabled = true,
    IsGpuEnabled = true
};
computer.Open();
// Access sensors via computer.Hardware collection
```

## Contributing

Contributions are welcome! Please feel free to submit pull requests.

### Areas for Improvement
- [ ] Add support for additional AIO displays
- [ ] GUI configuration tool
- [ ] Custom display themes
- [ ] macOS/Linux support

## License

This project is provided as-is for personal use. 

### Third-Party Licenses
- **CPUID SDK** - Proprietary (included DLLs)
- **LibreHardwareMonitor** - MPL 2.0
- **HidSharp** - Apache 2.0
- **Newtonsoft.Json** - MIT

## Acknowledgments

- [HWiNFO](https://www.hwinfo.com/) - Excellent hardware monitoring tool
- [LibreHardwareMonitor](https://github.com/LibreHardwareMonitor/LibreHardwareMonitor) - Open source hardware monitoring
- [CPUID](https://www.cpuid.com/) - Hardware monitoring SDK

## Changelog

### v2.1.0
- Added LibreHardwareMonitor as built-in fallback
- Added HWiNFO shared memory integration for AMD RDNA 4 GPUs
- Added GPU frequency display
- Fixed CPU temperature/usage swap issue
- Improved logging and diagnostics

### v2.0.0
- Initial release with GAMDIAS ATLAS support
