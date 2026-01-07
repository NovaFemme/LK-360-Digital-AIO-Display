# HID Device Communication Library (C#)

A pure C# library for communicating with USB HID devices to send system monitoring data (CPU, GPU, memory, disk, network stats, time, etc.). This is a port of the original native DLL.

## Requirements

- Windows 10/11
- .NET Framework 4.8 or .NET 6.0+ (Windows)
- Visual Studio 2019+ or .NET SDK

## Building

### Using Visual Studio

1. Open `HidDeviceLib.sln`
2. Build the solution (Ctrl+Shift+B)

### Using .NET CLI

```bash
dotnet build
```

### Building for specific framework

```bash
# .NET Framework 4.8
dotnet build -f net48

# .NET 6.0
dotnet build -f net6.0-windows

# .NET 8.0
dotnet build -f net8.0-windows
```

## Project Structure

```
HidDeviceLib/
├── HidDeviceLib.sln          # Solution file
├── HidDeviceLib.csproj       # Main library project
├── HidDevice.cs              # Main implementation (instance-based)
├── HidDeviceApi.cs           # Static API wrapper (DLL-compatible)
├── Example/
│   ├── HidDeviceExample.csproj
│   └── Program.cs            # Usage examples
└── README.md
```

## Usage

### Option 1: Static API (Drop-in replacement for P/Invoke)

If your existing code uses P/Invoke to call the native DLL, you can use the static `HidDeviceApi` class as a drop-in replacement:

```csharp
using HidDeviceLib;

// Configure USB device filter (VID/PID)
byte[] usbConfig = new byte[] { 0x01, 0x45, 0x10, 0x01 };
HidDeviceApi.SetCheckUsb(usbConfig, 4, 20);

// Connect to devices
HidDeviceApi.DeviceOpen();

// Set CPU info
byte[] cpuInfo = new byte[] { 65, 45, 0x0F, 0xA0 }; // 65°C, 45%, 4000MHz
HidDeviceApi.SetCpuDynamicInfo(cpuInfo);
HidDeviceApi.SetFanRPM(1500);

// Set GPU info
byte[] gpuInfo = new byte[] { 70, 80, 0x07, 0x08 }; // 70°C, 80%, 1800MHz
HidDeviceApi.SetGpuDynamicInfo(gpuInfo);

// Send all data to device
DateTime now = DateTime.Now;
byte[] timeData = new byte[]
{
    (byte)(now.Year >> 8), (byte)(now.Year & 0xFF),
    (byte)now.Month, (byte)now.Day, (byte)now.DayOfWeek,
    (byte)now.Hour, (byte)now.Minute, (byte)now.Second, 0
};
HidDeviceApi.SetDeviceTime(timeData);

// On application exit
HidDeviceApi.Shutdown();
```

### Option 2: Instance-based API

For better control and testability, use the `HidDevice` class directly:

```csharp
using HidDeviceLib;

using (var device = new HidDevice())
{
    byte[] usbConfig = new byte[] { 0x01, 0x45, 0x10, 0x01 };
    device.SetCheckUsb(usbConfig, 4, 20);
    device.DeviceOpen();

    if (device.GetOnlineDeviceQty() > 0)
    {
        device.SetCpuDynamicInfo(new byte[] { 65, 45, 0x0F, 0xA0 });
        device.SetGpuDynamicInfo(new byte[] { 70, 80, 0x07, 0x08 });
        // ... set other data ...
        device.SetDeviceTime(timeData);
    }
} // Automatically disposed
```

## Migration from Native DLL

If you're replacing P/Invoke calls to the native DLL:

### Before (P/Invoke)

```csharp
[DllImport("hid_device.dll")]
static extern int SetCpuDynamicInfo(byte[] data);

[DllImport("hid_device.dll")]
static extern int DeviceOpen();

// Usage
DeviceOpen();
SetCpuDynamicInfo(data);
```

### After (C# Library)

```csharp
using HidDeviceLib;

// Usage - just change the class name
HidDeviceApi.DeviceOpen();
HidDeviceApi.SetCpuDynamicInfo(data);
```

## API Reference

### Device Management

| Method | Description |
|--------|-------------|
| `SetCheckUsb(data, size, maxDevices)` | Configure USB VID/PID filters |
| `CheckOnlineDevice()` | Scan for connected devices |
| `GetOnlineDeviceQty()` | Get count of connected devices |
| `DeviceOpen()` | Open and connect to devices |
| `GetErrorCode()` | Get last error code |

### CPU Information

| Method | Description |
|--------|-------------|
| `SetCpuDynamicInfo(data)` | Set CPU temp (byte 0), usage % (byte 1), frequency (bytes 2-3) |
| `SetCpuVoltage(data)` | Set CPU voltage (bytes 1-2, in mV) |
| `SetCpuWatt(data)` | Set CPU power (bytes 1-2, in W) |
| `SetFanRPM(rpm)` | Set CPU fan speed |

### GPU Information

| Method | Description |
|--------|-------------|
| `SetGpuDynamicInfo(data)` | Set GPU temp, usage %, frequency |
| `SetGpuVoltage(data)` | Set GPU voltage |
| `SetGpuWatt(data)` | Set GPU power |
| `SetGpuFanRPM(rpm)` | Set GPU fan speed |

### Other

| Method | Description |
|--------|-------------|
| `SetMemDynamicInfo(data)` | Set memory temp, usage %, frequency |
| `SetDiskDynamicInfo(data)` | Set disk temp, usage % |
| `SetNetSpeed(upload, download)` | Set network speeds (bytes/sec) |
| `SetDisplayMode(mode, flags)` | Set display mode |
| `SetDeviceTime(data)` | Send time and all collected data to device |

## Data Formats

### CPU/GPU/Memory Dynamic Info (4 bytes)
```
Byte 0: Temperature (°C)
Byte 1: Usage (%)
Byte 2: Frequency low byte
Byte 3: Frequency high byte
```

### Voltage/Wattage (3 bytes)
```
Byte 0: 0
Byte 1: Value high byte
Byte 2: Value low byte
```

### Time Data (9 bytes)
```
Byte 0-1: Year (big-endian)
Byte 2: Month
Byte 3: Day
Byte 4: Day of week
Byte 5: Hour
Byte 6: Minute
Byte 7: Second
Byte 8: Reserved
```

## Configuration

Create a `config.ini` file in the application directory to enable debug logging:

```ini
[config]
IsLog=TRUE
```

Debug output will be written to `debug.txt`.

## Default USB Device

The library is configured by default to look for devices with:
- VID: `0x0145`
- PID: `0x1001`

Use `SetCheckUsb()` to configure different VID/PID values.

## Thread Safety

The library is thread-safe. All public methods use internal locking to protect shared state.

## Notes

- This is a pure C# port of the original native DLL
- No native dependencies required
- Compatible with .NET Framework 4.8 and .NET 6.0+
- Windows only (uses Windows HID APIs)
