using HardwareHelp;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using HidDeviceLib;

namespace XKWLib
{
    public class XKWLibHelp
    {
      private const uint retError = 0;
      private bool thread_flag;
      private Thread thread_usb_send;
      private int usb_send_delay = 500;
      private bool usb_conn_state;
      private int fefresh_delay = 500;
      public int localPort = 8890;
      private bool IsUdp = true;
      private Hardware hardware;
      private UsbNotificationHelp usbNotification;
      private static int lastWriteTime;
      
      // NEW: Track hardware initialization state
      private bool hardwareInitialized = false;
      private int hardwareInitRetries = 0;
      private const int MAX_HARDWARE_INIT_RETRIES = 10; // Give up after 10 tries
      
      // LibreHardwareMonitor fallback (built-in, preferred)
      private LHMReader lhmReader = null;
      private bool lhmInitialized = false;
      
      // HWiNFO fallback for GPU data (requires external app)
      private HWiNFOReader hwInfoReader = null;
      private bool hwInfoInitialized = false;

      public string serverName { get; private set; } = "";

      public bool IsNew { get; private set; }

      public void ReadConfig()
      {
        if (!File.Exists(ReadWriteINI.DefaultPath))
        {
          ReadWriteINI.WriteIniData("config", "fefresh_delay", this.fefresh_delay.ToString(), ReadWriteINI.DefaultPath);
          ReadWriteINI.WriteIniData("config", "send_delay", this.usb_send_delay.ToString(), ReadWriteINI.DefaultPath);
          ReadWriteINI.WriteIniData("config", "IsLog", Log.IsLog.ToString(), ReadWriteINI.DefaultPath);
          ReadWriteINI.WriteIniData("config", "localPort", this.localPort.ToString(), ReadWriteINI.DefaultPath);
          ReadWriteINI.WriteIniData("config", "LogPath", "", ReadWriteINI.DefaultPath);
          ReadWriteINI.WriteIniData("config", "IsUdp", this.IsUdp.ToString(), ReadWriteINI.DefaultPath);
        }
        int.TryParse(ReadWriteINI.ReadIniData("config", "fefresh_delay", this.fefresh_delay.ToString(), ReadWriteINI.DefaultPath), out this.fefresh_delay);
        int.TryParse(ReadWriteINI.ReadIniData("config", "send_delay", this.usb_send_delay.ToString(), ReadWriteINI.DefaultPath), out this.usb_send_delay);
        int.TryParse(ReadWriteINI.ReadIniData("config", "localPort", this.localPort.ToString(), ReadWriteINI.DefaultPath), out this.localPort);
        bool.TryParse(ReadWriteINI.ReadIniData("config", "IsLog", Log.IsLog.ToString(), ReadWriteINI.DefaultPath), out Log.IsLog);
        bool.TryParse(ReadWriteINI.ReadIniData("config", "IsUdp", this.IsUdp.ToString(), ReadWriteINI.DefaultPath), out this.IsUdp);
        try
        {
          Log.LogPath = AppDomain.CurrentDomain.BaseDirectory;
          Log.LogPath = ReadWriteINI.ReadIniData("config", "LogPath", Log.LogPath, ReadWriteINI.DefaultPath);
          if (!Directory.Exists(Log.LogPath))
            Directory.CreateDirectory(Log.LogPath);
          Log.LogPath = Path.Combine(Log.LogPath, "log.txt");
        }
        catch
        {
          Log.LogPath = AppDomain.CurrentDomain.BaseDirectory + "log.txt";
        }
      }

      private void Recv_Command_Click(UdpCommand command)
      {
        try
        {
          if (command.cmd == "rundata")
          {
            command.cmdType = 1;
            command.data = "";
            if (this.hardware != null)
            {
              lock (this.hardware.SystemData)
              {
                List<HardwareDataInfo.DataItem> systemRunData = this.hardware.SystemData.GetSystemRunData();
                if (systemRunData != null)
                {
                  JArray jarray = new JArray();
                  foreach (HardwareDataInfo.DataItem dataItem in systemRunData)
                    jarray.Add((JToken) dataItem.ToJson());
                  command.data = jarray.ToString();
                }
              }
            }
            UdpUtil.Singleton.Send(command, command.remoteEP);
          }
          else if (command.cmd == "staticdata")
          {
            command.cmdType = 1;
            command.data = "";
            if (this.hardware != null)
            {
              lock (this.hardware.SystemData)
              {
                List<HardwareDataInfo.DataItem> systemData = this.hardware.SystemData.GetSystemData();
                if (systemData != null)
                {
                  JArray jarray = new JArray();
                  foreach (HardwareDataInfo.DataItem dataItem in systemData)
                    jarray.Add((JToken) dataItem.ToJson());
                  command.data = jarray.ToString();
                }
              }
            }
            UdpUtil.Singleton.Send(command, command.remoteEP);
          }
          else if (command.cmd == "open")
          {
            this.Start("CPUID");
          }
          else
          {
            if (!(command.cmd == "stop"))
              return;
            this.Stop();
          }
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.ToString());
        }
      }

      private void UsbNotification()
      {
        try
        {
                //HidDeviceApi.DeviceClose();
                int num = (int)HidDeviceApi.DeviceOpen();
                HidDeviceApi.CheckOnlineDevice();
        }
        catch (Exception ex)
        {
          Log.WriteLog("UsbNotification error: " + ex.Message);
        }
      }

      public bool Start(string name)
      {
        try
        {
          this.ReadConfig();
          
          // Force logging for diagnostics
          Log.IsLog = true;
          Log.WriteLog("=== LK Digital Display Starting ===");
          Log.WriteLog("Service name: " + name);
          
          if (this.usbNotification == null)
          {
            this.usbNotification = new UsbNotificationHelp();
            this.usbNotification.callBack += new UsbNotificationHelp.CallBack_UsbNotification(this.UsbNotification);
          }
          if (this.IsUdp)
          {
            if (!UdpUtil.Singleton.Bind(this.localPort))
            {
              Log.WriteLog("The communication port is occupied, port number:" + this.localPort.ToString());
            }
            else
            {
              UdpUtil.Singleton.Command_Recv += new UdpUtil.Recv_Command_Click(this.Recv_Command_Click);
              UdpUtil.Singleton.BeginReceive();
            }
          }
          Log.WriteLog("name:=" + name);
          if (!PIDVIDHelp.SetCheckUsb(name))
          {
            Log.WriteLog("ERROR: PIDVIDHelp.SetCheckUsb failed for: " + name);
            throw new Exception("Device model reading failed");
          }
          Log.WriteLog("PIDVIDHelp.SetCheckUsb succeeded");
          
          this.IsNew = false;
          switch (name)
          {
            case "SystemTemperatureMonitoring":
              this.hardware = (Hardware) new CPUIDHardware();
              this.IsNew = true;
              break;
            case "XKWHardwareServices":
              this.hardware = (Hardware) new CPUIDHardware();
              this.IsNew = true;
              break;
            case "SLAYERCPULiveTemperature":
              this.hardware = (Hardware) new CPUIDHardware();
              this.IsNew = true;
              break;
            case "RM-Hardware":
              this.hardware = (Hardware) new CPUIDHardware();
              this.IsNew = true;
              break;
            case "LK Digital Display":
              this.hardware = (Hardware) new CPUIDHardware();
              this.IsNew = true;
              break;
            default:
              throw new Exception("Invalid service name");
          }
          this.serverName = name;
          this.thread_flag = true;
          this.hardwareInitialized = false;
          this.hardwareInitRetries = 0;
          
          this.thread_usb_send = new Thread(new ThreadStart(this.thread_thread));
          this.thread_usb_send.Priority = ThreadPriority.Highest;
          this.thread_usb_send.Start();
          Log.WriteLog("Service started successfully, worker thread launched");
          return true;
        }
        catch (Exception ex)
        {
          Log.IsLog = true;
          Log.WriteLog($"{MethodBase.GetCurrentMethod().DeclaringType.FullName}.{MethodBase.GetCurrentMethod().Name}:\r\n\t{ex.ToString()}");
          return false;
        }
      }

      public void Stop()
      {
        try
        {
          Log.WriteLog("Start to stop service");
          if (this.thread_flag)
            this.thread_flag = false;
          if (this.usbNotification != null)
          {
            this.usbNotification.Dispose();
            this.usbNotification = (UsbNotificationHelp) null;
          }
          if (this.thread_usb_send != null)
          {
            this.thread_usb_send.Join();
            this.thread_usb_send = (Thread) null;
          }
          if (this.hardware != null)
          {
            this.hardware.Dispose();
            this.hardware = (Hardware) null;
          }
          // Dispose HWiNFO reader
          if (this.hwInfoReader != null)
          {
            this.hwInfoReader.Dispose();
            this.hwInfoReader = null;
          }
          Log.WriteLog("Service has been shut down");
        }
        catch (Exception ex)
        {
        }
      }

      /// <summary>
      /// FIXED: Main worker thread - no longer gets stuck if hardware init fails
      /// </summary>
      private void thread_thread()
      {
        try
        {
          Log.WriteLog("Worker thread started");
          
          while (this.thread_flag)
          {
            // Try to initialize hardware if not done yet
            if (!this.hardwareInitialized)
            {
              if (this.hardware.InitState || this.hardware.Start())
              {
                if (this.hardware.OpenRefresh(this.fefresh_delay))
                {
                  this.hardwareInitialized = true;
                  Log.WriteLog("Hardware monitoring initialized successfully!");
                }
                else
                {
                  Log.WriteLog("hardware.OpenRefresh failed, retry " + this.hardwareInitRetries);
                  this.hardwareInitRetries++;
                }
              }
              else
              {
                this.hardwareInitRetries++;
                Log.WriteLog("hardware.Start() failed, retry " + this.hardwareInitRetries + "/" + MAX_HARDWARE_INIT_RETRIES);
                
                // After max retries, give up on hardware but continue with display
                if (this.hardwareInitRetries >= MAX_HARDWARE_INIT_RETRIES)
                {
                  Log.WriteLog("*** GIVING UP on hardware monitoring after " + MAX_HARDWARE_INIT_RETRIES + " retries ***");
                  Log.WriteLog("*** Display will continue with placeholder data ***");
                  // Don't set hardwareInitialized = true, but continue anyway
                }
              }
            }
            
            // ALWAYS try to send data to display, even if hardware isn't working
            // This ensures the display turns on and shows something
            this.SendComputerInfo();
            
            Thread.Sleep(this.usb_send_delay);
          }
          
          Log.WriteLog("Worker thread exiting");
        }
        catch (Exception ex)
        {
          Log.WriteLog("Worker thread exception: " + ex.ToString());
        }
      }

      private bool ConnectUsb()
      {
        try
        {
          if (!this.usb_conn_state)
          {
            Log.WriteLog("Attempting to open USB device...");
            uint result = HidDeviceApi.DeviceOpen();
            Log.WriteLog("HidDeviceApi.DeviceOpen() returned: " + result);
            
            if (result == 0U)
            {
              Log.WriteLog("USB Open failed!");
              throw new Exception(string.Format("USB Open fail"));
            }
            
            Log.WriteLog("USB device opened successfully, sending initial data...");
            this.SendDynamicData();
            this.usb_conn_state = true;
          }
          return this.usb_conn_state;
        }
        catch (Exception ex)
        {
          Log.WriteLog("ConnectUsb error: " + ex.Message);
          this.usb_conn_state = false;
          return false;
        }
      }

      public void SendDynamicDataNew()
      {
        string txt = "";
        try
        {
          int num1 = 0;
          DateTime now = DateTime.Now;
          byte[] pData1 = new byte[9];
          byte[] numArray1 = pData1;
          int index1 = num1;
          int num2 = index1 + 1;
          int num3 = (int) (byte) (now.Year / 100);
          numArray1[index1] = (byte) num3;
          byte[] numArray2 = pData1;
          int index2 = num2;
          int num4 = index2 + 1;
          int num5 = (int) (byte) (now.Year - (int) pData1[0] * 100);
          numArray2[index2] = (byte) num5;
          byte[] numArray3 = pData1;
          int index3 = num4;
          int num6 = index3 + 1;
          int month = (int) (byte) now.Month;
          numArray3[index3] = (byte) month;
          byte[] numArray4 = pData1;
          int index4 = num6;
          int num7 = index4 + 1;
          int day = (int) (byte) now.Day;
          numArray4[index4] = (byte) day;
          byte[] numArray5 = pData1;
          int index5 = num7;
          int num8 = index5 + 1;
          int dayOfWeek = (int) (byte) now.DayOfWeek;
          numArray5[index5] = (byte) dayOfWeek;
          byte[] numArray6 = pData1;
          int index6 = num8;
          int num9 = index6 + 1;
          int hour = (int) (byte) now.Hour;
          numArray6[index6] = (byte) hour;
          byte[] numArray7 = pData1;
          int index7 = num9;
          int num10 = index7 + 1;
          int minute = (int) (byte) now.Minute;
          numArray7[index7] = (byte) minute;
          byte[] numArray8 = pData1;
          int index8 = num10;
          int num11 = index8 + 1;
          int second = (int) (byte) now.Second;
          numArray8[index8] = (byte) second;
          byte[] numArray9 = pData1;
          int index9 = num11;
          int num12 = index9 + 1;
          int num13 = (int) (byte) (now.Millisecond / 10);
          numArray9[index9] = (byte) num13;
          
          uint timeResult = HidDeviceApi.SetDeviceTime(pData1);
          if (timeResult == 0U)
          {
            Log.WriteLog("SetDeviceTime failed!");
            throw new Exception("System time transmission failed");
          }
          txt += "Time Message sent successfully \t";
          
          int num14 = 0;
          
          // Get GPU data (with fallback to placeholder if hardware not working)
          HardwareDataInfo.DataItem systemRunData1 = null;
          if (this.hardwareInitialized && this.hardware != null)
          {
            try
            {
              systemRunData1 = this.hardware.SystemData.GetSystemRunData(HardwareDataInfo.DataType.Gpu);
              Log.WriteLog($"GPU data query returned: {(systemRunData1 != null ? systemRunData1.Size.ToString() + " entries" : "null")}");
            }
            catch (Exception ex)
            {
              Log.WriteLog("GPU data query exception: " + ex.Message);
            }
          }
          else
          {
            Log.WriteLog($"Hardware not ready for GPU query: initialized={this.hardwareInitialized}, hardware={(this.hardware != null ? "not null" : "null")}");
          }
          
          if (systemRunData1 != null && systemRunData1.Size > 0)
          {
            Dictionary<string, object> dictionary = (Dictionary<string, object>) null;
            if (systemRunData1.Size > 1)
            {
              // First try NVIDIA GPUs
              for (int index10 = 0; index10 < systemRunData1.Size; ++index10)
              {
                string gpuName = systemRunData1[index10]["name"].ToString().ToUpper();
                if (gpuName.IndexOf("GTX") >= 0 || gpuName.IndexOf("RTX") >= 0 || gpuName.IndexOf("NVIDIA") >= 0)
                {
                  dictionary = systemRunData1[index10];
                  Log.WriteLog("Found NVIDIA GPU: " + systemRunData1[index10]["name"].ToString());
                  break;
                }
              }
              // Then try AMD GPUs
              if (dictionary == null)
              {
                for (int index10 = 0; index10 < systemRunData1.Size; ++index10)
                {
                  string gpuName = systemRunData1[index10]["name"].ToString().ToUpper();
                  if (gpuName.IndexOf("RADEON") >= 0 || gpuName.IndexOf("AMD") >= 0 || gpuName.IndexOf("RX ") >= 0)
                  {
                    dictionary = systemRunData1[index10];
                    Log.WriteLog("Found AMD GPU: " + systemRunData1[index10]["name"].ToString());
                    break;
                  }
                }
              }
              // Then try Intel GPUs
              if (dictionary == null)
              {
                for (int index10 = 0; index10 < systemRunData1.Size; ++index10)
                {
                  string gpuName = systemRunData1[index10]["name"].ToString().ToUpper();
                  if (gpuName.IndexOf("INTEL") >= 0 || gpuName.IndexOf("UHD") >= 0 || gpuName.IndexOf("IRIS") >= 0)
                  {
                    dictionary = systemRunData1[index10];
                    Log.WriteLog("Found Intel GPU: " + systemRunData1[index10]["name"].ToString());
                    break;
                  }
                }
              }
              // Fallback: any device with required data
              if (dictionary == null)
              {
                for (int index11 = 0; index11 < systemRunData1.Size; ++index11)
                {
                  if (systemRunData1[index11].ContainsKey("clock") && systemRunData1[index11].ContainsKey("load") && systemRunData1[index11].ContainsKey("temperature"))
                  {
                    dictionary = systemRunData1[index11];
                    Log.WriteLog("Found GPU with required data: " + (systemRunData1[index11].ContainsKey("name") ? systemRunData1[index11]["name"].ToString() : "unknown"));
                    break;
                  }
                }
              }
            }
            if (dictionary == null && systemRunData1.Size > 0)
            {
              dictionary = systemRunData1[0];
              Log.WriteLog("Using first GPU entry: " + (dictionary.ContainsKey("name") ? dictionary["name"].ToString() : "unknown"));
            }
            
            if (dictionary != null)
            {
              byte[] pData2 = new byte[8];
              Array.Clear((Array) pData2, 0, pData2.Length);
              pData2[0] = !dictionary.ContainsKey("load") ? (byte) 0 : (byte) (int) float.Parse(dictionary["load"].ToString());
              pData2[1] = !dictionary.ContainsKey("temperature") ? (byte) 0 : (byte) (int) float.Parse(dictionary["temperature"].ToString());
              Log.WriteLog($"Set GPU: load={pData2[0]}%, temp={pData2[1]}°C");
              if (HidDeviceApi.SetGpuDynamicInfo(pData2) == 0U)
                throw new Exception("gpu Running information failed to be sent");
            }
            else
            {
              Log.WriteLog("No valid GPU dictionary found from CPUID, trying HWiNFO...");
              TryHWiNFOGpuFallback();
            }
          }
          else
          {
            Log.WriteLog("No GPU data available from CPUID hardware monitor, trying HWiNFO...");
            TryHWiNFOGpuFallback();
          }
          
          // Get CPU data (with fallback)
          HardwareDataInfo.DataItem systemRunData2 = null;
          if (this.hardwareInitialized && this.hardware != null)
          {
            try
            {
              systemRunData2 = this.hardware.SystemData.GetSystemRunData(HardwareDataInfo.DataType.Cpu);
            }
            catch { }
          }
          
          if (systemRunData2 != null && systemRunData2.Size > 0)
          {
            byte[] pData3 = new byte[systemRunData2.Size * 8];
            Array.Clear((Array) pData3, 0, pData3.Length);
            for (int index12 = 0; index12 < systemRunData2.Size; ++index12)
            {
              int num15 = (int) float.Parse(systemRunData2[index12]["clock"].ToString());
              pData3[8 * index12] = (byte) (int) float.Parse(systemRunData2[index12]["load"].ToString());
              pData3[8 * index12 + 1] = (byte) (int) float.Parse(systemRunData2[index12]["temperature"].ToString());
              pData3[8 * index12 + 2] = (byte) (num15 & (int) byte.MaxValue);
              pData3[8 * index12 + 3] = (byte) (num15 >> 8 & (int) byte.MaxValue);
            }
            Log.WriteLog($"Set CPU: load={pData3[0]}%, temp={pData3[1]}°C");
            if (HidDeviceApi.SetCpuDynamicInfo(pData3) == 0U)
              throw new Exception("CPU Message sending failed");
          }
          else
          {
            // Placeholder CPU data
            byte[] pData3 = new byte[8];
            pData3[0] = 0;
            pData3[1] = 0;
            HidDeviceApi.SetCpuDynamicInfo(pData3);
          }
          
          // Get RAM data (with fallback)
          HardwareDataInfo.DataItem systemRunData3 = null;
          if (this.hardwareInitialized && this.hardware != null)
          {
            try
            {
              systemRunData3 = this.hardware.SystemData.GetSystemRunData(HardwareDataInfo.DataType.Ram);
            }
            catch { }
          }
          
          if (systemRunData3 != null && systemRunData3.Size > 0)
          {
            byte[] pData4 = new byte[systemRunData3.Size * 8];
            Array.Clear((Array) pData4, 0, pData4.Length);
            for (int index13 = 0; index13 < systemRunData3.Size; ++index13)
            {
              float num16;
              float num17;
              if (systemRunData3[index13].ContainsKey("Memory Used") && systemRunData3[index13].ContainsKey("Memory Available"))
              {
                num16 = float.Parse(systemRunData3[index13]["Memory Available"].ToString());
                num17 = num16 + float.Parse(systemRunData3[index13]["Memory Used"].ToString());
              }
              else if (systemRunData3[index13].ContainsKey("Available Memory") && systemRunData3[index13].ContainsKey("Used Memory"))
              {
                num16 = float.Parse(systemRunData3[index13]["Available Memory"].ToString());
                num17 = num16 + float.Parse(systemRunData3[index13]["Used Memory"].ToString());
              }
              else
                continue;
              int num18 = (int) (((double) num17 != 0.0 ? (double) (num16 / num17) : 0.0) * 100.0);
              pData4[8 * index13] = (byte) (100 - num18);
            }
            if (HidDeviceApi.SetMemDynamicInfo(pData4) == 0U)
              throw new Exception("RAM Message sending failed");
          }
          else
          {
            byte[] pData4 = new byte[8];
            pData4[0] = 0;
            HidDeviceApi.SetMemDynamicInfo(pData4);
          }
          
          // Get Disk data (with fallback)
          HardwareDataInfo.DataItem systemRunData4 = null;
          if (this.hardwareInitialized && this.hardware != null)
          {
            try
            {
              systemRunData4 = this.hardware.SystemData.GetSystemRunData(HardwareDataInfo.DataType.HHD);
            }
            catch { }
          }
          
          if (systemRunData4 != null && systemRunData4.Size > 0)
          {
            byte[] pData5 = new byte[systemRunData4.Size * 8];
            Array.Clear((Array) pData5, 0, pData5.Length);
            for (int index14 = 0; index14 < systemRunData4.Size; ++index14)
            {
              int int32 = Convert.ToInt32(float.Parse(systemRunData4[index14]["load"].ToString()));
              pData5[8 * index14] = (byte) int32;
            }
            if (HidDeviceApi.SetDiskDynamicInfo(pData5) == 0U)
              throw new Exception("HHD Message sending failed");
          }
          else
          {
            byte[] pData5 = new byte[8];
            pData5[0] = 0;
            HidDeviceApi.SetDiskDynamicInfo(pData5);
          }
          
          // Get Fan data (with fallback)
          HardwareDataInfo.DataItem systemRunData5 = null;
          if (this.hardwareInitialized && this.hardware != null)
          {
            try
            {
              systemRunData5 = this.hardware.SystemData.GetSystemRunData(HardwareDataInfo.DataType.Fan);
            }
            catch { }
          }
          
          if (systemRunData5 != null && systemRunData5.Size > 0)
          {
            short result = 0;
            short.TryParse(systemRunData5[0]["speed"].ToString(), out result);
            if (HidDeviceApi.SetFanRPM(result) == 0U)
              throw new Exception("Fan information transmission failed");
          }
          else
          {
            HidDeviceApi.SetFanRPM(0);
          }
        }
        catch (Exception ex)
        {
          this.usb_conn_state = false;
          Log.WriteLog($"{MethodBase.GetCurrentMethod().DeclaringType.FullName}.{MethodBase.GetCurrentMethod().Name}:\r\n\t{ex.ToString()}");
        }
      }

      public void SendDynamicData()
      {
        try
        {
          if (this.IsNew)
          {
            this.SendDynamicDataNew();
          }
          else
          {
            int num1 = 0;
            DateTime now = DateTime.Now;
            byte[] pData1 = new byte[9];
            byte[] numArray1 = pData1;
            int index1 = num1;
            int num2 = index1 + 1;
            int num3 = (int) (byte) (now.Year / 100);
            numArray1[index1] = (byte) num3;
            byte[] numArray2 = pData1;
            int index2 = num2;
            int num4 = index2 + 1;
            int num5 = (int) (byte) (now.Year - (int) pData1[0] * 100);
            numArray2[index2] = (byte) num5;
            byte[] numArray3 = pData1;
            int index3 = num4;
            int num6 = index3 + 1;
            int month = (int) (byte) now.Month;
            numArray3[index3] = (byte) month;
            byte[] numArray4 = pData1;
            int index4 = num6;
            int num7 = index4 + 1;
            int day = (int) (byte) now.Day;
            numArray4[index4] = (byte) day;
            byte[] numArray5 = pData1;
            int index5 = num7;
            int num8 = index5 + 1;
            int dayOfWeek = (int) (byte) now.DayOfWeek;
            numArray5[index5] = (byte) dayOfWeek;
            byte[] numArray6 = pData1;
            int index6 = num8;
            int num9 = index6 + 1;
            int hour = (int) (byte) now.Hour;
            numArray6[index6] = (byte) hour;
            byte[] numArray7 = pData1;
            int index7 = num9;
            int num10 = index7 + 1;
            int minute = (int) (byte) now.Minute;
            numArray7[index7] = (byte) minute;
            byte[] numArray8 = pData1;
            int index8 = num10;
            int num11 = index8 + 1;
            int second = (int) (byte) now.Second;
            numArray8[index8] = (byte) second;
            byte[] numArray9 = pData1;
            int index9 = num11;
            int num12 = index9 + 1;
            int num13 = (int) (byte) (now.Millisecond / 10);
            numArray9[index9] = (byte) num13;
            if (HidDeviceApi.SetDeviceTime(pData1) == 0U)
              throw new Exception("System time transmission failed");
            
            // Similar fallback handling for old format...
            HardwareDataInfo.DataItem systemRunData1 = null;
            if (this.hardwareInitialized && this.hardware != null)
            {
              try { systemRunData1 = this.hardware.SystemData.GetSystemRunData(HardwareDataInfo.DataType.Gpu); } catch { }
            }
            
            if (systemRunData1 != null && systemRunData1.Size > 0)
            {
              Dictionary<string, object> dictionary = (Dictionary<string, object>) null;
              if (systemRunData1.Size > 1)
              {
                for (int index10 = 0; index10 < systemRunData1.Size; ++index10)
                {
                  if (systemRunData1[index10]["name"].ToString().ToUpper().IndexOf("GTX") >= 0 || systemRunData1[index10]["name"].ToString().ToUpper().IndexOf("RTX") >= 0 || systemRunData1[index10]["name"].ToString().ToUpper().IndexOf("NVIDIA") >= 0)
                  {
                    dictionary = systemRunData1[index10];
                    break;
                  }
                }
                if (dictionary == null)
                {
                  for (int index11 = 0; index11 < systemRunData1.Size; ++index11)
                  {
                    if (systemRunData1[index11].ContainsKey("clock") && systemRunData1[index11].ContainsKey("load") && systemRunData1[index11].ContainsKey("temperature"))
                    {
                      dictionary = systemRunData1[index11];
                      break;
                    }
                  }
                }
              }
              if (dictionary == null)
                dictionary = systemRunData1[0];
              byte[] pData2 = new byte[8];
              Array.Clear((Array) pData2, 0, pData2.Length);
              pData2[0] = !dictionary.ContainsKey("load") ? (byte) 0 : (byte) (int) float.Parse(dictionary["load"].ToString());
              pData2[1] = !dictionary.ContainsKey("temperature") ? (byte) 0 : (byte) (int) float.Parse(dictionary["temperature"].ToString());
              Log.WriteLog($"Set GPU temperature:{pData2[1]}");
              if (HidDeviceApi.SetGpuDynamicInfo(pData2) == 0U)
                throw new Exception("gpu Running information failed to be sent");
            }
            else
            {
              byte[] pData2 = new byte[8];
              HidDeviceApi.SetGpuDynamicInfo(pData2);
            }
            
            HardwareDataInfo.DataItem systemRunData2 = null;
            if (this.hardwareInitialized && this.hardware != null)
            {
              try { systemRunData2 = this.hardware.SystemData.GetSystemRunData(HardwareDataInfo.DataType.Cpu); } catch { }
            }
            
            if (systemRunData2 != null && systemRunData2.Size > 0)
            {
              byte[] pData3 = new byte[systemRunData2.Size * 8];
              Array.Clear((Array) pData3, 0, pData3.Length);
              for (int index12 = 0; index12 < systemRunData2.Size; ++index12)
              {
                int num14 = (int) float.Parse(systemRunData2[index12]["clock"].ToString());
                pData3[8 * index12] = (byte) (int) float.Parse(systemRunData2[index12]["load"].ToString());
                pData3[8 * index12 + 1] = (byte) (int) float.Parse(systemRunData2[index12]["temperature"].ToString());
                pData3[8 * index12 + 2] = (byte) (num14 & (int) byte.MaxValue);
                pData3[8 * index12 + 3] = (byte) (num14 >> 8 & (int) byte.MaxValue);
              }
              Log.WriteLog($"Set CPU temperature:{pData3[1]}");
              if (HidDeviceApi.SetCpuDynamicInfo(pData3) == 0U)
                throw new Exception("CPU Message sending failed");
            }
            else
            {
              byte[] pData3 = new byte[8];
              HidDeviceApi.SetCpuDynamicInfo(pData3);
            }
            
            HardwareDataInfo.DataItem systemRunData3 = null;
            if (this.hardwareInitialized && this.hardware != null)
            {
              try { systemRunData3 = this.hardware.SystemData.GetSystemRunData(HardwareDataInfo.DataType.Ram); } catch { }
            }
            
            if (systemRunData3 != null && systemRunData3.Size > 0)
            {
              byte[] pData4 = new byte[systemRunData3.Size * 8];
              Array.Clear((Array) pData4, 0, pData4.Length);
              for (int index13 = 0; index13 < systemRunData3.Size; ++index13)
              {
                float num15;
                float num16;
                if (systemRunData3[index13].ContainsKey("Memory Used") && systemRunData3[index13].ContainsKey("Memory Available"))
                {
                  num15 = float.Parse(systemRunData3[index13]["Memory Available"].ToString());
                  num16 = num15 + float.Parse(systemRunData3[index13]["Memory Used"].ToString());
                }
                else if (systemRunData3[index13].ContainsKey("Available Memory") && systemRunData3[index13].ContainsKey("Used Memory"))
                {
                  num15 = float.Parse(systemRunData3[index13]["Available Memory"].ToString());
                  num16 = num15 + float.Parse(systemRunData3[index13]["Used Memory"].ToString());
                }
                else
                  continue;
                int num17 = (int) (((double) num16 != 0.0 ? (double) (num15 / num16) : 0.0) * 100.0);
                pData4[8 * index13] = (byte) (100 - num17);
              }
              if (HidDeviceApi.SetMemDynamicInfo(pData4) == 0U)
                throw new Exception("RAM Message sending failed");
            }
            else
            {
              byte[] pData4 = new byte[8];
              HidDeviceApi.SetMemDynamicInfo(pData4);
            }
            
            HardwareDataInfo.DataItem systemRunData4 = null;
            if (this.hardwareInitialized && this.hardware != null)
            {
              try { systemRunData4 = this.hardware.SystemData.GetSystemRunData(HardwareDataInfo.DataType.HHD); } catch { }
            }
            
            if (systemRunData4 != null && systemRunData4.Size > 0)
            {
              byte[] pData5 = new byte[systemRunData4.Size * 8];
              Array.Clear((Array) pData5, 0, pData5.Length);
              for (int index14 = 0; index14 < systemRunData4.Size; ++index14)
              {
                int int32 = Convert.ToInt32(float.Parse(systemRunData4[index14]["load"].ToString()));
                pData5[8 * index14] = (byte) int32;
              }
              if (HidDeviceApi.SetDiskDynamicInfo(pData5) == 0U)
                throw new Exception("HHD Message sending failed");
            }
            else
            {
              byte[] pData5 = new byte[8];
              HidDeviceApi.SetDiskDynamicInfo(pData5);
            }
            
            HardwareDataInfo.DataItem systemRunData5 = null;
            if (this.hardwareInitialized && this.hardware != null)
            {
              try { systemRunData5 = this.hardware.SystemData.GetSystemRunData(HardwareDataInfo.DataType.Fan); } catch { }
            }
            
            if (systemRunData5 != null && systemRunData5.Size > 0)
            {
              short result = 0;
              short.TryParse(systemRunData5[0]["speed"].ToString(), out result);
              if (HidDeviceApi.SetFanRPM(result) == 0U)
                throw new Exception("Fan information transmission failed");
            }
            else
            {
              HidDeviceApi.SetFanRPM(0);
            }
          }
        }
        catch (Exception ex)
        {
          this.usb_conn_state = false;
          Log.WriteLog($"{MethodBase.GetCurrentMethod().DeclaringType.FullName}.{MethodBase.GetCurrentMethod().Name}:\r\n\t{ex.ToString()}");
        }
      }

      public void SendComputerInfo()
      {
        try
        {
          if (!this.ConnectUsb())
          {
            Log.WriteLog("ConnectUsb returned false");
            return;
          }
          this.SendDynamicData();
        }
        catch (Exception ex)
        {
          Log.WriteLog("SendComputerInfo error: " + ex.Message);
        }
      }

      /// <summary>
      /// Try to get GPU data from HWiNFO shared memory as fallback
      /// </summary>
      private bool lhmDetailedLogDone = false;
      private bool hwInfoDetailedLogDone = false;
      private int hwInfoLogCount = 0;  // Log first 5 calls for debugging
      private LHMReader.GpuData lastLhmPartialData = null;  // Store partial LHM data as fallback
      
      private void TryHWiNFOGpuFallback()
      {
        // Try LibreHardwareMonitor first (built-in, no external app needed)
        var lhmResult = TryLHMGpuFallback();
        
        if (lhmResult == LhmResult.Success)
          return;  // LHM worked with full data (temp + usage)
        
        // If LHM has partial data (usage but no temp), try HWiNFO for complete data
        if (lhmResult == LhmResult.PartialData)
        {
          if (TryHWiNFOOnly())
            return;  // HWiNFO worked, use it
          
          // HWiNFO failed, use LHM's partial data (better than nothing)
          UseLhmPartialData();
          return;
        }
        
        // LHM completely failed, try HWiNFO
        TryHWiNFOOnly();
      }
      
      private enum LhmResult
      {
        Failed,       // No data at all
        PartialData,  // Has usage but no temperature
        Success       // Has both temp and usage
      }
      
      /// <summary>
      /// Try to get GPU data from LibreHardwareMonitor (built-in)
      /// </summary>
      private LhmResult TryLHMGpuFallback()
      {
        try
        {
          // Initialize LHM reader if not done yet
          if (!lhmInitialized)
          {
            lhmReader = new LHMReader();
            lhmInitialized = true;
            Log.WriteLog("LibreHardwareMonitor initialized, available: " + lhmReader.IsAvailable);
          }

          if (lhmReader != null && lhmReader.IsAvailable)
          {
            // Use logging callback only first time to dump sensor info
            Action<string> logCallback = null;
            if (!lhmDetailedLogDone)
            {
              logCallback = (msg) => Log.WriteLog(msg);
              lhmDetailedLogDone = true;
            }
            
            var gpuData = lhmReader.GetGpuData(logCallback);
            if (gpuData != null && gpuData.IsValid)
            {
              // Check if we have complete data (temp + usage) or only partial (usage only)
              if (!gpuData.HasTemperature)
              {
                // Store partial data as fallback
                lastLhmPartialData = gpuData;
                Log.WriteLog($"LHM GPU ({gpuData.Name}): usage={gpuData.Usage}% (no temp sensor - trying HWiNFO)");
                return LhmResult.PartialData;
              }
              
              // Full data available - use it
              byte[] pData = new byte[8];
              pData[0] = (byte)Math.Min(100, Math.Max(0, (int)gpuData.Usage));
              pData[1] = (byte)Math.Min(255, Math.Max(0, (int)gpuData.Temperature));
              
              // GPU Frequency in MHz (bytes 2-3, little endian)
              ushort gpuFreqMHz = (ushort)Math.Min(65535, Math.Max(0, (int)gpuData.CoreClock));
              pData[2] = (byte)(gpuFreqMHz & 0xFF);        // Low byte
              pData[3] = (byte)((gpuFreqMHz >> 8) & 0xFF); // High byte
              
              Log.WriteLog($"LHM GPU ({gpuData.Name}): load={pData[0]}%, temp={pData[1]}°C, freq={gpuFreqMHz}MHz");
              
              if (HidDeviceApi.SetGpuDynamicInfo(pData) == 0U)
              {
                Log.WriteLog("Failed to send LHM GPU data");
              }
              
              // Also set GPU fan if available
              if (gpuData.FanRpm > 0)
              {
                HidDeviceApi.SetGpuFanRPM((short)gpuData.FanRpm);
              }
              
              // Also set GPU power if available  
              if (gpuData.Power > 0)
              {
                byte[] powerData = new byte[3];
                ushort watts = (ushort)gpuData.Power;
                powerData[0] = 0;
                powerData[1] = (byte)(watts >> 8);
                powerData[2] = (byte)(watts & 0xFF);
                HidDeviceApi.SetGpuWatt(powerData);
              }
              
              return LhmResult.Success;
            }
            else
            {
              if (!lhmDetailedLogDone)
                Log.WriteLog("LibreHardwareMonitor returned no valid GPU data, trying HWiNFO...");
            }
          }
          else
          {
            Log.WriteLog("LibreHardwareMonitor not available, trying HWiNFO...");
          }
        }
        catch (Exception ex)
        {
          Log.WriteLog("LibreHardwareMonitor error: " + ex.Message + ", trying HWiNFO...");
        }
        
        return LhmResult.Failed;
      }
      
      /// <summary>
      /// Use LHM's partial data (usage only, no temp) as last resort
      /// </summary>
      private void UseLhmPartialData()
      {
        if (lastLhmPartialData == null)
        {
          byte[] emptyData = new byte[8];
          HidDeviceApi.SetGpuDynamicInfo(emptyData);
          return;
        }
        
        byte[] pData = new byte[8];
        pData[0] = (byte)Math.Min(100, Math.Max(0, (int)lastLhmPartialData.Usage));
        pData[1] = 0;  // No temperature available
        
        // GPU Frequency in MHz (bytes 2-3, little endian) - may be available from D3D
        ushort gpuFreqMHz = (ushort)Math.Min(65535, Math.Max(0, (int)lastLhmPartialData.CoreClock));
        pData[2] = (byte)(gpuFreqMHz & 0xFF);        // Low byte
        pData[3] = (byte)((gpuFreqMHz >> 8) & 0xFF); // High byte
        
        Log.WriteLog($"Using LHM partial data: load={pData[0]}%, temp=N/A, freq={gpuFreqMHz}MHz");
        
        if (HidDeviceApi.SetGpuDynamicInfo(pData) == 0U)
        {
          Log.WriteLog("Failed to send LHM partial GPU data");
        }
      }
      
      /// <summary>
      /// Try to get GPU data from HWiNFO shared memory (requires external app)
      /// </summary>
      private bool TryHWiNFOOnly()
      {
        try
        {
          // Initialize HWiNFO reader if not done yet
          if (!hwInfoInitialized)
          {
            hwInfoReader = new HWiNFOReader();
            hwInfoInitialized = true;
            Log.WriteLog("HWiNFO reader initialized, available: " + hwInfoReader.IsAvailable);
          }

          if (hwInfoReader != null && hwInfoReader.IsAvailable)
          {
            // Use logging callback for first 5 calls to help debug sensor selection
            Action<string> logCallback = null;
            if (hwInfoLogCount < 5)
            {
              logCallback = (msg) => Log.WriteLog(msg);
              hwInfoLogCount++;
            }
            
            var gpuData = hwInfoReader.GetGpuData(logCallback);
            if (gpuData != null && gpuData.IsValid)
            {
              byte[] pData = new byte[8];
              pData[0] = (byte)Math.Min(100, Math.Max(0, (int)gpuData.Usage));
              pData[1] = (byte)Math.Min(255, Math.Max(0, (int)gpuData.Temperature));
              
              // GPU Frequency in MHz (bytes 2-3, little endian)
              ushort gpuFreqMHz = (ushort)Math.Min(65535, Math.Max(0, (int)gpuData.CoreClock));
              pData[2] = (byte)(gpuFreqMHz & 0xFF);        // Low byte
              pData[3] = (byte)((gpuFreqMHz >> 8) & 0xFF); // High byte
              
              Log.WriteLog($"HWiNFO GPU ({gpuData.Name}): load={pData[0]}%, temp={pData[1]}°C, freq={gpuFreqMHz}MHz");
              
              if (HidDeviceApi.SetGpuDynamicInfo(pData) == 0U)
              {
                Log.WriteLog("Failed to send HWiNFO GPU data");
              }
              
              // Also set GPU fan if available
              if (gpuData.FanRpm > 0)
              {
                HidDeviceApi.SetGpuFanRPM((short)gpuData.FanRpm);
              }
              
              // Also set GPU power if available  
              if (gpuData.Power > 0)
              {
                byte[] powerData = new byte[3];
                ushort watts = (ushort)gpuData.Power;
                powerData[0] = 0;
                powerData[1] = (byte)(watts >> 8);
                powerData[2] = (byte)(watts & 0xFF);
                HidDeviceApi.SetGpuWatt(powerData);
              }
              
              return true;  // Success
            }
            else
            {
              Log.WriteLog("HWiNFO returned no valid GPU data");
            }
          }
          else
          {
            if (!hwInfoInitialized || hwInfoReader == null)
            {
              Log.WriteLog("HWiNFO reader not initialized");
            }
            else
            {
              Log.WriteLog("HWiNFO shared memory not available - make sure HWiNFO is running with 'Shared Memory Support' enabled");
            }
          }
        }
        catch (Exception ex)
        {
          Log.WriteLog("HWiNFO fallback error: " + ex.Message);
        }
        
        return false;  // Failed
      }
    }
}
