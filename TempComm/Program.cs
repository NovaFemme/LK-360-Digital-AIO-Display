using System;
using System.Threading;
using HidDeviceLib;

namespace HidDeviceDiagnostic
{
    /// <summary>
    /// Diagnostic test program to identify HID device issues
    /// Run this as Administrator to test device detection
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("===========================================");
            Console.WriteLine("   LK Digital Display - Diagnostic Tool");
            Console.WriteLine("===========================================\n");

            // Check if running as admin
            bool isAdmin = new System.Security.Principal.WindowsPrincipal(
                System.Security.Principal.WindowsIdentity.GetCurrent())
                .IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
            
            Console.WriteLine("Running as Administrator: " + (isAdmin ? "YES" : "NO (may cause issues!)"));
            Console.WriteLine("64-bit Process: " + Environment.Is64BitProcess);
            Console.WriteLine("Base Directory: " + AppDomain.CurrentDomain.BaseDirectory);
            Console.WriteLine();

            try
            {
                Console.WriteLine("Step 1: Creating HidDevice instance...");
                Console.WriteLine("        (This will enumerate all HID devices and log to hid_debug.txt)");
                Console.WriteLine();
                
                using (var device = new HidDevice())
                {
                    Console.WriteLine("Step 2: Setting USB filter for LK Digital Display");
                    Console.WriteLine("        VID=0x0145 (325), PID=0x1005 (4101)");
                    
                    // LK Digital Display: VID=0x0145, PID=0x1005
                    byte[] usbConfig = new byte[] { 0x01, 0x45, 0x10, 0x05 };
                    device.SetCheckUsb(usbConfig, 4, 20);
                    Console.WriteLine();

                    Console.WriteLine("Step 3: Opening device connection...");
                    int result = device.DeviceOpen();
                    Console.WriteLine("        DeviceOpen() returned: " + result);
                    Console.WriteLine();

                    int deviceCount = device.GetOnlineDeviceQty();
                    Console.WriteLine("Step 4: Connected devices: " + deviceCount);
                    Console.WriteLine();

                    if (deviceCount > 0)
                    {
                        Console.WriteLine("SUCCESS! Device connected.");
                        Console.WriteLine();
                        Console.WriteLine("Step 5: Sending test data...");

                        // Send some test data
                        device.SetCpuDynamicInfo(new byte[] { 50, 25, 0x0F, 0xA0 }); // 50°C, 25%, 4000MHz
                        device.SetGpuDynamicInfo(new byte[] { 60, 50, 0x07, 0x08 }); // 60°C, 50%, 1800MHz
                        device.SetMemDynamicInfo(new byte[] { 40, 45, 0x0C, 0x80 }); // 40°C, 45%, 3200MHz
                        device.SetDiskDynamicInfo(new byte[] { 35, 60 }); // 35°C, 60% used
                        device.SetNetSpeed(512000, 2048000);
                        device.SetFanRPM(1200);
                        device.SetGpuFanRPM(1500);

                        DateTime now = DateTime.Now;
                        byte[] timeData = new byte[]
                        {
                            (byte)(now.Year / 100),
                            (byte)(now.Year % 100),
                            (byte)now.Month,
                            (byte)now.Day,
                            (byte)now.DayOfWeek,
                            (byte)now.Hour,
                            (byte)now.Minute,
                            (byte)now.Second,
                            (byte)(now.Millisecond / 10)
                        };
                        
                        int sendResult = device.SetDeviceTime(timeData);
                        Console.WriteLine("        SetDeviceTime() returned: " + sendResult);

                        if (sendResult > 0)
                        {
                            Console.WriteLine();
                            Console.WriteLine("*** DATA SENT SUCCESSFULLY! ***");
                            Console.WriteLine("Your display should now show the test data.");
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("WARNING: SetDeviceTime returned 0 - data may not have been sent.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("PROBLEM: No devices connected!");
                        Console.WriteLine();
                        Console.WriteLine("Possible causes:");
                        Console.WriteLine("  1. AIO cooler display is not connected via USB");
                        Console.WriteLine("  2. Device has different VID/PID than expected");
                        Console.WriteLine("  3. Device driver not installed");
                        Console.WriteLine("  4. Another application is using the device");
                        Console.WriteLine();
                        Console.WriteLine("Check hid_debug.txt for a list of ALL HID devices on your system.");
                        Console.WriteLine("Look for your AIO cooler and note its VID/PID values.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine("ERROR: " + ex.Message);
                Console.WriteLine();
                Console.WriteLine("Stack trace:");
                Console.WriteLine(ex.StackTrace);
            }

            Console.WriteLine();
            Console.WriteLine("===========================================");
            Console.WriteLine("Check these files for detailed logs:");
            Console.WriteLine("  - hid_debug.txt  (HID device enumeration)");
            Console.WriteLine("  - log.txt        (Application logs)");
            Console.WriteLine("===========================================");
            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
