using System;
using System.Collections.Generic;
using System.Reflection;
using HidDeviceLib;

namespace XKWLib
{
    public class PIDVIDHelp
    {

      public static readonly int deviceNum = 20;

      public static bool SetCheckUsb(string name)
      {
        try
        {
          Log.WriteLog("PIDVIDHelp.SetCheckUsb called with name: " + name);
          
          byte[] pidvidBytes = PIDVIDHelp.PIDVID.GetPIDVIDBytes(name);
          if (pidvidBytes == null || pidvidBytes.Length < 4)
          {
            Log.WriteLog("ERROR: GetPIDVIDBytes returned null or < 4 bytes for: " + name);
            throw new Exception("Error retrieving pid and vid");
          }
          
          // Log the VID/PID values being set
          if (pidvidBytes.Length >= 4)
          {
            int vid = (pidvidBytes[0] << 8) | pidvidBytes[1];
            int pid = (pidvidBytes[2] << 8) | pidvidBytes[3];
            Log.WriteLog(string.Format("Setting USB filter: VID=0x{0:X4} ({0}), PID=0x{1:X4} ({1})", vid, pid));
          }
          
          HidDeviceApi.SetCheckUsb(pidvidBytes, pidvidBytes.Length, PIDVIDHelp.deviceNum);
          Log.WriteLog("HidDeviceApi.SetCheckUsb completed successfully");
          return true;
        }
        catch (Exception ex)
        {
          Log.WriteLog($"{MethodBase.GetCurrentMethod().DeclaringType.FullName}.{MethodBase.GetCurrentMethod().Name}:\r\n\t{ex.ToString()}");
          return false;
        }
      }

      public class PIDVID
      {
        public uint pid;
        public uint vid;

        public PIDVID(uint pid, uint vid)
        {
          this.pid = pid;
          this.vid = vid;
        }

        public byte[] GetBytes()
        {
          return new byte[4]
          {
            (byte) (this.vid >> 8),
            (byte) this.vid,
            (byte) (this.pid >> 8),
            (byte) this.pid
          };
        }

        public static PIDVIDHelp.PIDVID[] GetPIDVID(string str)
        {
          List<PIDVIDHelp.PIDVID> pidvidList = new List<PIDVIDHelp.PIDVID>();
          switch (str)
          {
            case "SystemTemperatureMonitoring":
              pidvidList.Add(new PIDVIDHelp.PIDVID(4097U, 325U));  // PID=0x1001, VID=0x0145
              break;
            case "XKWHardwareServices":
              pidvidList.Add(new PIDVIDHelp.PIDVID(4097U, 4791U)); // PID=0x1001, VID=0x12B7
              break;
            case "ZALMAN-CTM":
              pidvidList.Add(new PIDVIDHelp.PIDVID(16081U, 7255U));
              pidvidList.Add(new PIDVIDHelp.PIDVID(19343U, 7255U));
              pidvidList.Add(new PIDVIDHelp.PIDVID(14975U, 7255U));
              break;
            case "ZALMAN-TDM":
              pidvidList.Add(new PIDVIDHelp.PIDVID(16082U, 7255U));
              pidvidList.Add(new PIDVIDHelp.PIDVID(16086U, 7255U));
              break;
            case "SLAYERCPULiveTemperature":
              pidvidList.Add(new PIDVIDHelp.PIDVID(4097U, 11365U));
              break;
            case "RM-Hardware":
              pidvidList.Add(new PIDVIDHelp.PIDVID(4096U /*0x1000*/, 11365U));
              break;
            case "LK Digital Display":
              // GAMDIAS ATLAS Digital Display
              // VID=0x1B80 (7040), PID=0xB538 (46392)
              pidvidList.Add(new PIDVIDHelp.PIDVID(0xB538, 0x1B80));
              // Also try HWCX device which may be the data endpoint
              // VID=0x0145 (325), PID=0x1005 (4101)
              pidvidList.Add(new PIDVIDHelp.PIDVID(0x1005, 0x0145));
              break;
          }
          
          Log.WriteLog("GetPIDVID for '" + str + "' returned " + pidvidList.Count + " entries");
          foreach (var pv in pidvidList)
          {
            Log.WriteLog(string.Format("  VID=0x{0:X4}, PID=0x{1:X4}", pv.vid, pv.pid));
          }
          
          return pidvidList.ToArray();
        }

        public static byte[] GetPIDVIDBytes(string str)
        {
          PIDVIDHelp.PIDVID[] pidvid = PIDVIDHelp.PIDVID.GetPIDVID(str);
          if (pidvid == null || pidvid.Length == 0)
            return new byte[0];
          byte[] destinationArray = new byte[pidvid.Length * 4];
          for (int index = 0; index < pidvid.Length; ++index)
            Array.Copy((Array) pidvid[index].GetBytes(), 0, (Array) destinationArray, index * 4, 4);
          return destinationArray;
        }
      }
    }
}
