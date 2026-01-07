using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace XKWLib
{
    public class ReadWriteINI
    {
      public static string DefaultPath => AppDomain.CurrentDomain.BaseDirectory + "config.ini";

      [DllImport("kernel32")]
      private static extern long WritePrivateProfileString(
        string section,
        string key,
        string val,
        string filePath);

      [DllImport("kernel32")]
      private static extern long GetPrivateProfileString(
        string section,
        string key,
        string def,
        StringBuilder retVal,
        int size,
        string filePath);

      [DllImport("kernel32")]
      public static extern bool WritePrivateProfileString(
        byte[] section,
        byte[] key,
        byte[] val,
        string filePath);

      [DllImport("kernel32")]
      public static extern int GetPrivateProfileString(
        byte[] section,
        byte[] key,
        byte[] def,
        byte[] retVal,
        int size,
        string filePath);

      private static bool IsUTF8Bytes(byte[] data)
      {
        int num1 = 1;
        for (int index = 0; index < data.Length; ++index)
        {
          byte num2 = data[index];
          if (num1 == 1)
          {
            if (num2 >= (byte) 128 /*0x80*/)
            {
              while (((int) (num2 <<= 1) & 128 /*0x80*/) != 0)
                ++num1;
              if (num1 == 1 || num1 > 6)
                return false;
            }
          }
          else
          {
            if (((int) num2 & 192 /*0xC0*/) != 128 /*0x80*/)
              return false;
            --num1;
          }
        }
        if (num1 > 1)
          throw new Exception("Unexpected byte format");
        return true;
      }

      public static Encoding GetType(string file)
      {
        using (FileStream input = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        {
          byte[] numArray = new byte[3]
          {
            (byte) 254,
            byte.MaxValue,
            (byte) 0
          };

          Encoding type = Encoding.Default;
          using (BinaryReader binaryReader = new BinaryReader((Stream) input, Encoding.Default))
          {
            int result;
            int.TryParse(input.Length.ToString(), out result);
            byte[] data = binaryReader.ReadBytes(result);
            if (ReadWriteINI.IsUTF8Bytes(data) || data[0] == (byte) 239 && data[1] == (byte) 187 && data[2] == (byte) 191)
              type = Encoding.UTF8;
            else if (data[0] == (byte) 254 && data[1] == byte.MaxValue && data[2] == (byte) 0)
              type = Encoding.BigEndianUnicode;
            else if (data[0] == byte.MaxValue && data[1] == (byte) 254 && data[2] == (byte) 65)
              type = Encoding.Unicode;
            binaryReader.Close();
          }
          input.Close();
          input.Dispose();
          return type;
        }
      }

      private static byte[] getBytes(string s, Encoding encoding)
      {
        return s != null ? encoding.GetBytes(s) : (byte[]) null;
      }

      public static string ReadIniData(string Section, string Key, string NoText, string iniFilePath)
      {
        try
        {
          return ReadWriteINI.ReadIniData(Section, Key, NoText, iniFilePath, ReadWriteINI.GetType(iniFilePath));
        }
        catch
        {
          return NoText;
        }
      }

      public static string ReadIniData(
        string Section,
        string Key,
        string NoText,
        string iniFilePath,
        Encoding encoding)
      {
        try
        {
          byte[] numArray = new byte[1024 /*0x0400*/];
          int privateProfileString = ReadWriteINI.GetPrivateProfileString(ReadWriteINI.getBytes(Section, encoding), ReadWriteINI.getBytes(Key, encoding), ReadWriteINI.getBytes(NoText, encoding), numArray, numArray.Length, iniFilePath);
          return privateProfileString > 0 ? encoding.GetString(numArray, 0, privateProfileString).Trim() : NoText;
        }
        catch
        {
          return NoText;
        }
      }

      public static bool WriteIniData(string Section, string Key, string Value, string iniFilePath)
      {
        try
        {
          Encoding encoding = File.Exists(iniFilePath) ? ReadWriteINI.GetType(iniFilePath) : Encoding.Default;
          return ReadWriteINI.WriteIniData(Section, Key, Value, iniFilePath, encoding);
        }
        catch
        {
          return false;
        }
      }

      public static bool WriteIniData(
        string Section,
        string Key,
        string Value,
        string iniFilePath,
        Encoding encoding)
      {
        try
        {
          if (!File.Exists(iniFilePath))
            File.Create(iniFilePath).Dispose();
          encoding.GetBytes(Value);
          return ReadWriteINI.WritePrivateProfileString(encoding.GetBytes(Section), encoding.GetBytes(Key), encoding.GetBytes(Value), iniFilePath);
        }
        catch
        {
          return false;
        }
      }
    }
}
