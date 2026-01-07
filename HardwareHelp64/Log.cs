using System;
using System.IO;

namespace HardwareHelp
{
    public class Log
    {
      public static bool IsLog = false;
      public static string LogPath = AppDomain.CurrentDomain.BaseDirectory + "log.txt";

      public static void WriteLog(string txt)
      {
        try
        {
          if (!Log.IsLog)
            return;
          File.AppendAllText(Log.LogPath, $"{DateTime.Now}\t{txt}\r\n");
        }
        catch
        {
        }
      }
    }
}
