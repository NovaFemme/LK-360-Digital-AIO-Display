using System;
using System.IO.MemoryMappedFiles;
using System.Threading;

namespace HardwareHelp
{
    public class SharedMemoryManager
    {
      public const int DefaultCapacity = 10485760 /*0xA00000*/;
      private bool createNew;

      public int MaxSize { get; private set; }

      public string Name { get; private set; }

      public int lastWriteTime { get; private set; }

      private string GetMutexName() => this.Name + "_mutex";

      private string GetMmfName() => this.Name + "_mmf";

      public bool CreateNew
      {
        get => this.createNew;
        set => this.createNew = value;
      }

      public MemoryMappedFile _mmf { get; private set; }

      public Mutex _mutex { get; private set; }

      public SharedMemoryManager(string name, int maxSize = 10485760 /*0xA00000*/)
      {
        this.Name = name;
        this.MaxSize = maxSize;
        this._mutex = new Mutex(false, this.GetMutexName(), out this.createNew);
        if (!this.createNew)
          Thread.Sleep(100);
        this._mutex.WaitOne();
        this._mmf = MemoryMappedFile.CreateOrOpen(this.GetMmfName(), (long) this.MaxSize);
        if (this.createNew)
          new SharedMemoryData((string) null).WriteMemory(this._mmf);
        this._mutex.ReleaseMutex();
      }

      public void Write(byte[] data)
      {
        try
        {
          this._mutex.WaitOne();
          new SharedMemoryData(data).WriteMemory(this._mmf);
          this.lastWriteTime = Environment.TickCount;
        }
        catch (Exception ex)
        {
        }
        finally
        {
          this._mutex.ReleaseMutex();
        }
      }

      public void Write(string data)
      {
        try
        {
          this._mutex.WaitOne();
          new SharedMemoryData(data).WriteMemory(this._mmf);
          this.lastWriteTime = Environment.TickCount;
        }
        catch (Exception ex)
        {
        }
        finally
        {
          this._mutex.ReleaseMutex();
        }
      }

      public SharedMemoryData Read()
      {
        try
        {
          this._mutex.WaitOne();
          return SharedMemoryData.ReadMemory(this._mmf);
        }
        catch (Exception ex)
        {
          return (SharedMemoryData) null;
        }
        finally
        {
          this._mutex.ReleaseMutex();
        }
      }
    }
}
