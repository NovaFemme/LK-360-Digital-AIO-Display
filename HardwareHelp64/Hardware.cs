using System;
using System.Collections.Generic;
using System.Management;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;

namespace HardwareHelp
{
    public class Hardware
    {
      public static readonly int timeout = 20;
      private static Random random = new Random((int) DateTime.Now.Ticks);
      protected HardwareDataInfo _systemData;
      private Thread _threadRefresh;
      private bool _autoRefresh;
      private int _delay = 1000;
      protected bool _initState;
      private object dataLock = new object();
      private float _lastCpuTemperature;
      protected SharedMemoryManager sharedSystem;
      protected SharedMemoryManager sharedRunData;
      protected string _name = "__Hardware_V" + Assembly.GetExecutingAssembly().GetName().Version.ToString();

      public static string GetMmfName(string name) => $"{name}_mmf0";

      public static string GetMutexName(string name) => $"{name}_mutex";

      public static byte[] GetDateTimeTick() => BitConverter.GetBytes((uint) DateTime.Now.Ticks);

      protected static float GetNextTemperature(float old_val, float new_val, float size)
      {
        try
        {
          int num = (double) new_val - (double) size <= (double) old_val ? ((double) new_val + (double) size >= (double) old_val ? Hardware.random.Next(-1, 1) : Hardware.random.Next(0, 1)) : Hardware.random.Next(-1, 0);
          return new_val + (float) num;
        }
        catch (Exception ex)
        {
          return new_val;
        }
      }

      public HardwareDataInfo SystemData
      {
        get
        {
          lock (this.dataLock)
            return this._systemData;
        }
      }

      protected Hardware()
      {
        this._systemData = new HardwareDataInfo();
        this._initState = false;
      }

      protected virtual void ImitateData()
      {
        try
        {
          lock (this.SystemData)
          {
            HardwareDataInfo.DataItem systemRunData = this.SystemData.GetSystemRunData(HardwareDataInfo.DataType.Cpu);
            if (systemRunData == null || systemRunData.Size < 0)
              return;
            foreach (Dictionary<string, object> dictionary in systemRunData)
            {
              if (dictionary["name"].ToString() == "packge")
              {
                float nextTemperature = Hardware.GetNextTemperature(this._lastCpuTemperature, float.Parse(dictionary["temperature"].ToString()), 5f);
                dictionary["temperature"] = (object) nextTemperature;
              }
            }
          }
        }
        catch (Exception ex)
        {
        }
      }

      private void IsWrite()
      {
        if (this.sharedRunData.Read().tick <= this.sharedRunData.lastWriteTime)
          return;
        this.sharedRunData.CreateNew = false;
      }

      private void thread_AutoRefresh()
      {
        while (this._autoRefresh)
        {
          this.Refresh();
          Thread.Sleep(this._delay);
        }
      }

      public virtual bool ReadRunData()
      {
        try
        {
          SharedMemoryData sharedMemoryData = this.sharedRunData.Read();
          if (this.sharedRunData.CreateNew && sharedMemoryData.tick > this.sharedRunData.lastWriteTime)
            this.sharedRunData.CreateNew = false;
          if (this.sharedRunData.CreateNew)
          {
            bool flag = true;
            lock (this.SystemData)
            {
              List<Dictionary<string, object>> cpuRunInfo = this.GetCpuRunInfo();
              if (cpuRunInfo == null)
                flag = false;
              else
                this.SystemData.AddSystemRunData(new HardwareDataInfo.DataItem(HardwareDataInfo.DataType.Cpu, cpuRunInfo));
              List<Dictionary<string, object>> gpuRunInfo = this.GetGpuRunInfo();
              if (gpuRunInfo != null)
                this.SystemData.AddSystemRunData(new HardwareDataInfo.DataItem(HardwareDataInfo.DataType.Gpu, gpuRunInfo));
              List<Dictionary<string, object>> ramRunInfo = this.GetRAMRunInfo();
              if (ramRunInfo != null)
                this.SystemData.AddSystemRunData(new HardwareDataInfo.DataItem(HardwareDataInfo.DataType.Ram, ramRunInfo));
              List<Dictionary<string, object>> fanSpeed = this.GetFanSpeed();
              if (fanSpeed != null)
                this.SystemData.AddSystemRunData(new HardwareDataInfo.DataItem(HardwareDataInfo.DataType.Fan, fanSpeed));
              List<Dictionary<string, object>> network = this.GetNetwork();
              if (network != null)
                this.SystemData.AddSystemRunData(new HardwareDataInfo.DataItem(HardwareDataInfo.DataType.Network, network));
              this.sharedRunData.Write(this.SystemData.ToSystemRunString());
              return flag;
            }
          }
          if (Environment.TickCount - sharedMemoryData.tick >= this._delay * 5)
          {
            this.sharedRunData._mutex.WaitOne();
            try
            {
              if (Environment.TickCount - this.sharedRunData.Read().tick >= this._delay * 3)
              {
                this.sharedRunData.CreateNew = true;
                this.sharedRunData.Write(this.SystemData.ToSystemRunString());
              }
            }
            catch (Exception ex)
            {
            }
            finally
            {
              this.sharedRunData._mutex.ReleaseMutex();
            }
          }
          else
            this.SystemData.SetData(sharedMemoryData.GetString());
          return true;
        }
        catch (Exception ex)
        {
          return false;
        }
      }

      public virtual bool Start()
      {
        if (this.InitState)
          return this.InitState;
        int num = this.Init() ? 1 : 0;
        if (num == 0)
          return num != 0;
        if (this.sharedRunData != null)
          return num != 0;
        this.sharedRunData = new SharedMemoryManager(this._name + "_RunInfo");
        if (!this.sharedRunData.CreateNew)
          return num != 0;
        this.sharedRunData.Write((byte[]) null);
        return num != 0;
      }

      public virtual void Stop() => this.CloseRefresh();

      public bool InitState => this._initState;

      protected virtual bool Init()
      {
        try
        {
          if (!this._initState)
            this._initState = this.ReadSystemData();
          return this._initState;
        }
        catch (Exception ex)
        {
          this.Dispose();
          this._initState = false;
          Log.WriteLog($"{MethodBase.GetCurrentMethod().DeclaringType.FullName}.{MethodBase.GetCurrentMethod().Name}:\r\n\t{ex.ToString()}");
        }
        return this._initState;
      }

      public virtual void Dispose()
      {
        try
        {
          this.Stop();
        }
        catch
        {
        }
      }

      public bool ReadSystemData()
      {
        try
        {
          if (this.sharedSystem == null)
          {
            this.sharedSystem = new SharedMemoryManager(this._name + "_SystemInfo");
            if (this.sharedSystem.CreateNew)
            {
              List<Dictionary<string, object>> operatingSystem = this.GetOperatingSystem();
              if (operatingSystem != null)
                this._systemData.AddSystemData(new HardwareDataInfo.DataItem(HardwareDataInfo.DataType.SystemOS, operatingSystem));
              List<Dictionary<string, object>> baseBoard = this.GetBaseBoard();
              if (baseBoard != null)
                this._systemData.AddSystemData(new HardwareDataInfo.DataItem(HardwareDataInfo.DataType.BaseBoard, baseBoard));
              List<Dictionary<string, object>> cpuInfo = this.GetCpuInfo();
              if (cpuInfo != null)
                this._systemData.AddSystemData(new HardwareDataInfo.DataItem(HardwareDataInfo.DataType.Cpu, cpuInfo));
              List<Dictionary<string, object>> videoController = this.GetVideoController();
              if (videoController != null)
                this._systemData.AddSystemData(new HardwareDataInfo.DataItem(HardwareDataInfo.DataType.Gpu, videoController));
              List<Dictionary<string, object>> physicalMemory = this.GetPhysicalMemory();
              if (physicalMemory != null)
                this._systemData.AddSystemData(new HardwareDataInfo.DataItem(HardwareDataInfo.DataType.Ram, physicalMemory));
              List<Dictionary<string, object>> win32DiskDrive = this.GetWin32_DiskDrive();
              if (win32DiskDrive != null)
                this._systemData.AddSystemData(new HardwareDataInfo.DataItem(HardwareDataInfo.DataType.HHD, win32DiskDrive));
              List<Dictionary<string, object>> displayManufacturer = this.GetDisplayManufacturer();
              if (displayManufacturer != null)
                this._systemData.AddSystemData(new HardwareDataInfo.DataItem(HardwareDataInfo.DataType.Display, displayManufacturer));
              this.sharedSystem.Write(this._systemData.ToSystemDataString());
            }
            else
            {
              SharedMemoryData sharedMemoryData;
              do
              {
                sharedMemoryData = this.sharedSystem.Read();
              }
              while (sharedMemoryData.Size <= 0);
              this.SystemData.SetData(sharedMemoryData.GetString());
            }
          }
          return true;
        }
        catch (Exception ex)
        {
          return false;
        }
      }

      public bool OpenRefresh(int delay)
      {
        try
        {
          if (this._threadRefresh != null)
            this.CloseRefresh();
          this._delay = delay;
          this._autoRefresh = true;
          this._threadRefresh = new Thread(new ThreadStart(this.thread_AutoRefresh));
          this._threadRefresh.Priority = ThreadPriority.Highest;
          this._threadRefresh.Start();
        }
        catch (Exception ex)
        {
          Log.WriteLog($"{MethodBase.GetCurrentMethod().DeclaringType.FullName}.{MethodBase.GetCurrentMethod().Name}:\r\n\t{ex.ToString()}");
          this._autoRefresh = false;
        }
        return true;
      }

      public void CloseRefresh()
      {
        try
        {
          if (this._autoRefresh)
            this._autoRefresh = false;
          if (this._threadRefresh == null)
            return;
          this._threadRefresh.Abort();
          this._threadRefresh = (Thread) null;
        }
        catch (Exception ex)
        {
        }
      }

      public bool Refresh()
      {
        try
        {
          if (!this.ReadRunData())
          {
            this.ImitateData();
            return false;
          }
          foreach (Dictionary<string, object> dictionary in this.SystemData.GetSystemRunData(HardwareDataInfo.DataType.Cpu))
          {
            if (dictionary["name"].ToString() == "packge")
            {
              this._lastCpuTemperature = float.Parse(dictionary["temperature"].ToString());
              break;
            }
          }
          return true;
        }
        catch (Exception ex)
        {
          return false;
        }
      }

      public static List<Dictionary<string, object>> GetManagementBase(string path)
      {
        try
        {
          using (ManagementClass managementClass = new ManagementClass(path))
          {
            using (ManagementObjectCollection instances = managementClass.GetInstances())
            {
              List<Dictionary<string, object>> managementBase = new List<Dictionary<string, object>>();
              foreach (ManagementObject managementObject in instances)
              {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                foreach (PropertyData property in managementObject.Properties)
                {
                  if (managementObject[property.Name] != null)
                    dictionary.Add(property.Name, (object) managementObject[property.Name].ToString());
                }
                managementBase.Add(dictionary);
              }
              return managementBase;
            }
          }
        }
        catch (Exception ex)
        {
          return (List<Dictionary<string, object>>) null;
        }
      }

      public List<Dictionary<string, object>> GetCpuInfo()
      {
        List<Dictionary<string, object>> managementBase = Hardware.GetManagementBase("Win32_Processor");
        List<Dictionary<string, object>> cpuInfo = new List<Dictionary<string, object>>();
        if (managementBase != null)
        {
          foreach (Dictionary<string, object> dictionary1 in managementBase)
          {
            Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
            if (dictionary1.ContainsKey("Name"))
              dictionary2["Name"] = dictionary1["Name"];
            if (dictionary1.ContainsKey("NumberOfEnabledCore"))
              dictionary2["NumberOfEnabledCore"] = dictionary1["NumberOfEnabledCore"];
            if (dictionary1.ContainsKey("NumberOfLogicalProcessors"))
              dictionary2["NumberOfLogicalProcessors"] = dictionary1["NumberOfLogicalProcessors"];
            if (dictionary1.ContainsKey("ProcessorId"))
              dictionary2["ProcessorId"] = dictionary1["ProcessorId"];
            if (dictionary2.Keys.Count > 1)
              cpuInfo.Add(dictionary2);
          }
        }
        return cpuInfo;
      }

      public List<Dictionary<string, object>> GetVideoController()
      {
        List<Dictionary<string, object>> managementBase = Hardware.GetManagementBase("Win32_VideoController");
        List<Dictionary<string, object>> videoController = new List<Dictionary<string, object>>();
        if (managementBase != null)
        {
          foreach (Dictionary<string, object> dictionary1 in managementBase)
          {
            Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
            if (dictionary1.ContainsKey("Caption"))
              dictionary2["Caption"] = dictionary1["Caption"];
            if (dictionary1.ContainsKey("CurrentNumberOfColors"))
              dictionary2["CurrentNumberOfColors"] = dictionary1["CurrentNumberOfColors"];
            if (dictionary1.ContainsKey("DriverVersion"))
              dictionary2["DriverVersion"] = dictionary1["DriverVersion"];
            if (dictionary2.Keys.Count > 1)
              videoController.Add(dictionary2);
          }
        }
        return videoController;
      }

      public List<Dictionary<string, object>> GetOperatingSystem()
      {
        List<Dictionary<string, object>> managementBase = Hardware.GetManagementBase("Win32_OperatingSystem");
        List<Dictionary<string, object>> operatingSystem = new List<Dictionary<string, object>>();
        if (managementBase != null)
        {
          foreach (Dictionary<string, object> dictionary1 in managementBase)
          {
            Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
            if (dictionary1.ContainsKey("Caption"))
              dictionary2["Caption"] = dictionary1["Caption"];
            operatingSystem.Add(dictionary2);
          }
        }
        return operatingSystem;
      }

      public List<Dictionary<string, object>> GetBaseBoard()
      {
        List<Dictionary<string, object>> managementBase = Hardware.GetManagementBase("Win32_BaseBoard");
        List<Dictionary<string, object>> baseBoard = new List<Dictionary<string, object>>();
        if (managementBase != null)
        {
          foreach (Dictionary<string, object> dictionary1 in managementBase)
          {
            Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
            if (dictionary1.ContainsKey("SerialNumber"))
              dictionary2["SerialNumber"] = dictionary1["SerialNumber"];
            if (dictionary1.ContainsKey("Manufacturer"))
              dictionary2["Manufacturer"] = dictionary1["Manufacturer"];
            if (dictionary1.ContainsKey("Product"))
              dictionary2["Product"] = dictionary1["Product"];
            if (dictionary1.ContainsKey("Version"))
              dictionary2["Version"] = dictionary1["Version"];
            if (dictionary2.Keys.Count > 1)
              baseBoard.Add(dictionary2);
          }
        }
        return baseBoard;
      }

      public List<Dictionary<string, object>> GetPhysicalMemory()
      {
        List<Dictionary<string, object>> managementBase = Hardware.GetManagementBase("Win32_PhysicalMemory");
        List<Dictionary<string, object>> physicalMemory = new List<Dictionary<string, object>>();
        if (managementBase != null)
        {
          foreach (Dictionary<string, object> dictionary1 in managementBase)
          {
            Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
            if (dictionary1.ContainsKey("Manufacturer"))
              dictionary2["Manufacturer"] = dictionary1["Manufacturer"];
            if (dictionary1.ContainsKey("PartNumber"))
              dictionary2["PartNumber"] = dictionary1["PartNumber"];
            if (dictionary1.ContainsKey("Capacity"))
              dictionary2["Capacity"] = dictionary1["Capacity"];
            if (dictionary2.Keys.Count > 1)
              physicalMemory.Add(dictionary2);
          }
        }
        return physicalMemory;
      }

      public List<Dictionary<string, object>> GetWin32_DiskDrive()
      {
        List<Dictionary<string, object>> managementBase = Hardware.GetManagementBase("Win32_DiskDrive");
        List<Dictionary<string, object>> win32DiskDrive = new List<Dictionary<string, object>>();
        if (managementBase != null)
        {
          foreach (Dictionary<string, object> dictionary1 in managementBase)
          {
            Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
            if (dictionary1.ContainsKey("Caption"))
              dictionary2["Caption"] = dictionary1["Caption"];
            if (dictionary1.ContainsKey("FirmwareRevision"))
              dictionary2["FirmwareRevision"] = dictionary1["FirmwareRevision"];
            if (dictionary1.ContainsKey("Size"))
              dictionary2["Size"] = dictionary1["Size"];
            if (dictionary2.Keys.Count > 1)
              win32DiskDrive.Add(dictionary2);
          }
        }
        return win32DiskDrive;
      }

      public List<Dictionary<string, object>> GetDisplayManufacturer()
      {
        try
        {
          using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("\\\\.\\ROOT\\WMI", "Select * from WmiMonitorID"))
          {
            using (ManagementObjectCollection objectCollection = managementObjectSearcher.Get())
            {
              List<Dictionary<string, object>> displayManufacturer = new List<Dictionary<string, object>>();
              foreach (ManagementObject managementObject in objectCollection)
              {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                foreach (PropertyData property in managementObject.Properties)
                {
                  if (property.Name == "InstanceName")
                  {
                    string[] strArray = managementObject[property.Name].ToString().Split('\\');
                    if (strArray.Length >= 2)
                      dictionary.Add(property.Name, (object) strArray[1]);
                  }
                }
                displayManufacturer.Add(dictionary);
              }
              return displayManufacturer;
            }
          }
        }
        catch (Exception ex)
        {
          return (List<Dictionary<string, object>>) null;
        }
      }

      public virtual List<Dictionary<string, object>> GetFanSpeed()
      {
        return (List<Dictionary<string, object>>) null;
      }

      public virtual List<Dictionary<string, object>> GetCpuRunInfo()
      {
        return (List<Dictionary<string, object>>) null;
      }

      public virtual List<Dictionary<string, object>> GetGpuRunInfo()
      {
        return (List<Dictionary<string, object>>) null;
      }

      public virtual List<Dictionary<string, object>> GetRAMRunInfo()
      {
        try
        {
          double totalGB;
          double freeGB;
          double usedGB;
          Hardware.GetTotalPhysicalMemory(out totalGB, out freeGB, out usedGB);
          return new List<Dictionary<string, object>>()
          {
            new Dictionary<string, object>()
            {
              {
                "Used Memory",
                (object) (usedGB / 1024.0 / 1024.0 / 1024.0)
              },
              {
                "Available Memory",
                (object) (freeGB / 1024.0 / 1024.0 / 1024.0)
              },
              {
                "Memory",
                (object) (totalGB / 1024.0 / 1024.0 / 1024.0)
              }
            }
          };
        }
        catch (Exception ex)
        {
          return (List<Dictionary<string, object>>) null;
        }
      }

      public virtual List<Dictionary<string, object>> GetNetwork()
      {
        try
        {
          List<Dictionary<string, object>> network = new List<Dictionary<string, object>>();
          foreach (ManagementObject managementObject in new ManagementObjectSearcher("SELECT * FROM Win32_PerfFormattedData_Tcpip_NetworkInterface").Get())
            network.Add(new Dictionary<string, object>()
            {
              {
                "name",
                (object) managementObject["Name"].ToString()
              },
              {
                "ReceivedPerSec",
                (object) Convert.ToUInt64(managementObject["BytesReceivedPerSec"])
              },
              {
                "SentPerSec",
                (object) Convert.ToUInt64(managementObject["BytesSentPerSec"])
              }
            });
          return network;
        }
        catch (Exception ex)
        {
          return (List<Dictionary<string, object>>) null;
        }
      }

      private static void GetTotalPhysicalMemory(
        out double totalGB,
        out double freeGB,
        out double usedGB)
      {
        Hardware.MEMORYSTATUSEX lpBuffer = new Hardware.MEMORYSTATUSEX();
        totalGB = Hardware.GlobalMemoryStatusEx(lpBuffer) ? (double) lpBuffer.ullTotalPhys : throw new Exception("Unable to get total physical memory.");
        freeGB = (double) lpBuffer.ullAvailPhys;
        usedGB = totalGB - freeGB;
      }

      [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      private static extern bool GlobalMemoryStatusEx([In, Out] Hardware.MEMORYSTATUSEX lpBuffer);

      [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
      private class MEMORYSTATUSEX
      {
        public uint dwLength;
        public uint dwMemoryLoad;
        public ulong ullTotalPhys;
        public ulong ullAvailPhys;
        public ulong ullTotalPageFile;
        public ulong ullAvailPageFile;
        public ulong ullTotalVirtual;
        public ulong ullAvailVirtual;
        public ulong ullAvailExtendedVirtual;

        public MEMORYSTATUSEX()
        {
          this.dwLength = (uint) Marshal.SizeOf(typeof (Hardware.MEMORYSTATUSEX));
        }
      }
    }
}
