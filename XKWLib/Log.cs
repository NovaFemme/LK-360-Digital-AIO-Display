using System;
using System.IO;

namespace XKWLib
{
    public class Log
    {
      public static bool IsLog;
      public static string LogPath;

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
