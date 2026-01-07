using System;
using System.IO.MemoryMappedFiles;
using System.Text;

namespace HardwareHelp
{
    public class SharedMemoryData
    {
      private const int headLeng = 255 /*0xFF*/;
      private byte[] buffer;

      public int tick { get; private set; }

      public int Size => this.buffer == null ? 0 : this.buffer.Length;

      public SharedMemoryData(string data)
      {
        this.buffer = data != null ? Encoding.UTF8.GetBytes(data) : (byte[]) null;
        this.tick = Environment.TickCount;
      }

      public SharedMemoryData(string data, int tick)
      {
        this.buffer = Encoding.UTF8.GetBytes(data);
        this.tick = tick;
      }

      public SharedMemoryData(byte[] data)
      {
        this.buffer = data;
        this.tick = Environment.TickCount;
      }

      public SharedMemoryData(byte[] data, int tick)
      {
        this.buffer = data;
        this.tick = tick;
      }

      public byte[] GetBytes() => this.buffer;

      public string GetString()
      {
        return this.buffer == null || this.buffer.Length < 1 ? (string) null : Encoding.UTF8.GetString(this.buffer);
      }

      private byte[] GetWriteBuffer()
      {
        int length = this.Size + (int) byte.MaxValue;
        int destinationIndex1 = 0;
        byte[] destinationArray = new byte[length];
        byte[] bytes1 = BitConverter.GetBytes(Environment.TickCount);
        Array.Copy((Array) bytes1, 0, (Array) destinationArray, destinationIndex1, bytes1.Length);
        int destinationIndex2 = destinationIndex1 + bytes1.Length;
        byte[] bytes2 = BitConverter.GetBytes(this.Size);
        Array.Copy((Array) bytes2, 0, (Array) destinationArray, destinationIndex2, bytes2.Length);
        int num = destinationIndex2 + bytes2.Length;
        if (this.Size > 0)
          Array.Copy((Array) this.buffer, 0, (Array) destinationArray, (int) byte.MaxValue, this.buffer.Length);
        return destinationArray;
      }

      public bool WriteMemory(MemoryMappedFile memory)
      {
        try
        {
          using (MemoryMappedViewStream viewStream = memory.CreateViewStream())
          {
            byte[] writeBuffer = this.GetWriteBuffer();
            viewStream.Position = 0L;
            viewStream.Write(writeBuffer, 0, writeBuffer.Length);
          }
          return true;
        }
        catch (Exception ex)
        {
          return false;
        }
      }

      public static SharedMemoryData ReadMemory(MemoryMappedFile memory)
      {
        try
        {
          using (MemoryMappedViewStream viewStream = memory.CreateViewStream())
          {
            byte[] buffer = new byte[(int) byte.MaxValue];
            viewStream.Position = 0L;
            viewStream.Read(buffer, 0, buffer.Length);
            int int32_1 = BitConverter.ToInt32(buffer, 0);
            int int32_2 = BitConverter.ToInt32(buffer, 4);
            byte[] numArray = (byte[]) null;
            if (int32_2 > 0)
            {
              numArray = new byte[int32_2];
              viewStream.Read(numArray, 0, int32_2);
            }
            return new SharedMemoryData(numArray, int32_1);
          }
        }
        catch (Exception ex)
        {
          return (SharedMemoryData) null;
        }
      }
    }
}
