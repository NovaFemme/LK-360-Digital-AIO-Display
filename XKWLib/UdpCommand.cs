using System;
using System.Net;
using System.Text;
using System.Threading;

namespace XKWLib
{
    public class UdpCommand
    {
      public string cmd;
      public string data;
      public int cmdNo;
      public int cmdType;
      public IPEndPoint remoteEP;
      private static readonly byte[] startBuff = new byte[4]
      {
        byte.MaxValue,
        (byte) 254,
        byte.MaxValue,
        (byte) 253
      };
      private static readonly byte[] endBuff = new byte[4]
      {
        byte.MaxValue,
        (byte) 254,
        byte.MaxValue,
        (byte) 253
      };
      private static int _cmdNo = 0;
      private static Mutex mutex = new Mutex();

      public UdpCommand() => this.cmdNo = UdpCommand.GetNextCmdNo();

      public UdpCommand(string cmd, string data)
      {
        this.cmd = cmd;
        this.data = data;
        this.cmdNo = UdpCommand.GetNextCmdNo();
      }

      public UdpCommand(string cmd, string data, int cmdNo)
      {
        this.cmd = cmd;
        this.data = data;
        this.cmdNo = cmdNo;
      }

      public byte[] GetCmdBytes() => UdpCommand.Combination(this);

      public override string ToString() => $"cmdNo:{this.cmdNo} cmd:{this.cmd} data:{this.data}";

      public static int GetNextCmdNo()
      {
        try
        {
          UdpCommand.mutex.WaitOne();
          ++UdpCommand._cmdNo;
        }
        catch (Exception ex)
        {
          UdpCommand._cmdNo = 0;
        }
        int cmdNo = UdpCommand._cmdNo;
        UdpCommand.mutex.ReleaseMutex();
        return cmdNo;
      }

      public static byte[] Combination(UdpCommand udpCommand)
      {
        return UdpCommand.Combination(udpCommand.cmd, udpCommand.data, udpCommand.cmdType, udpCommand.cmdNo);
      }

      public static byte[] Combination(string cmd, string data, int cmdType)
      {
        return UdpCommand.Combination(cmd, data, cmdType, UdpCommand.GetNextCmdNo());
      }

      public static byte[] Combination(string cmd, string data, int cmdType, int cmdNo)
      {
        if (string.IsNullOrEmpty(data))
          data = " ";
        byte[] bytes = Encoding.UTF8.GetBytes($"{cmd}|{data}");
        byte[] destinationArray = new byte[bytes.Length + UdpCommand.startBuff.Length + UdpCommand.endBuff.Length + 3];
        int destinationIndex1 = 0;
        Array.Copy((Array) UdpCommand.startBuff, 0, (Array) destinationArray, destinationIndex1, UdpCommand.startBuff.Length);
        int num1 = destinationIndex1 + UdpCommand.startBuff.Length;
        byte[] numArray1 = destinationArray;
        int index1 = num1;
        int num2 = index1 + 1;
        int num3 = (int) (byte) cmdType;
        numArray1[index1] = (byte) num3;
        byte[] numArray2 = destinationArray;
        int index2 = num2;
        int num4 = index2 + 1;
        int num5 = (int) (byte) (cmdNo >> 8 & (int) byte.MaxValue);
        numArray2[index2] = (byte) num5;
        byte[] numArray3 = destinationArray;
        int index3 = num4;
        int destinationIndex2 = index3 + 1;
        int num6 = (int) (byte) (cmdNo & (int) byte.MaxValue);
        numArray3[index3] = (byte) num6;
        Array.Copy((Array) bytes, 0, (Array) destinationArray, destinationIndex2, bytes.Length);
        int destinationIndex3 = destinationIndex2 + bytes.Length;
        Array.Copy((Array) UdpCommand.endBuff, 0, (Array) destinationArray, destinationIndex3, UdpCommand.endBuff.Length);
        return destinationArray;
      }

      public static UdpCommand Analysis(byte[] buffer)
      {
        try
        {
          if (buffer.Length < UdpCommand.startBuff.Length + UdpCommand.endBuff.Length + 3)
            throw new Exception("less than minimum length");
          for (int index = 0; index < UdpCommand.startBuff.Length; ++index)
          {
            if ((int) buffer[index] != (int) UdpCommand.startBuff[index])
              throw new Exception("Data is illegal");
          }
          for (int index = 0; index < UdpCommand.endBuff.Length; ++index)
          {
            if ((int) buffer[buffer.Length - buffer.Length + index] != (int) UdpCommand.endBuff[index])
              throw new Exception("Data is illegal");
          }
          int length1 = UdpCommand.startBuff.Length;
          byte[] numArray = buffer;
          int index1 = length1;
          int index2 = index1 + 1;
          int num1 = (int) numArray[index1];
          int cmdNo = (int) buffer[index2] << 8 | (int) buffer[index2 + 1];
          int index3 = index2 + 2;
          string str1 = Encoding.UTF8.GetString(buffer, index3, buffer.Length - UdpCommand.startBuff.Length - UdpCommand.endBuff.Length - 3);
          int num2 = str1.IndexOf('|');
          if (num2 < 2)
            throw new Exception("illegal data");
          string str2 = str1;
          int length2 = num2;
          int startIndex = length2 + 1;
          return new UdpCommand(str2.Substring(0, length2), str1.Substring(startIndex, str1.Length - startIndex), cmdNo)
          {
            cmdType = num1
          };
        }
        catch
        {
          return (UdpCommand) null;
        }
      }
    }
}
