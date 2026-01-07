using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace XKWLib
{
    public class UdpUtil
    {
      private static readonly Lazy<UdpUtil> Instancelock = new Lazy<UdpUtil>((Func<UdpUtil>) (() => new UdpUtil()));
      public int buffSize = 20;
      private List<UdpCommand> recvList = new List<UdpCommand>();
      public static readonly int defaultPort = 7888;
      private UdpClient udpClient;
      private int localPort;
      public Recv_Click recv_Click;
      public Recv_Command_Click Command_Recv;
      private Mutex mutex = new Mutex();

      public static UdpUtil Singleton => UdpUtil.Instancelock.Value;

      private void AddCommand(UdpCommand udpCommand)
      {
        try
        {
          this.mutex.WaitOne();
          while (this.recvList.Count >= this.buffSize)
            this.recvList.RemoveAt(0);
          this.recvList.Add(udpCommand);
        }
        catch
        {
        }
        finally
        {
          this.mutex.ReleaseMutex();
        }
      }

      public UdpCommand GetCommand(int cmdNo)
      {
        try
        {
          this.mutex.WaitOne();
          UdpCommand command = (UdpCommand) null;
          for (int index = 0; index < this.recvList.Count; ++index)
          {
            if (this.recvList[index].cmdNo == cmdNo)
            {
              command = this.recvList[index];
              this.recvList.Remove(command);
              break;
            }
          }
          return command;
        }
        catch
        {
          return (UdpCommand) null;
        }
        finally
        {
          this.mutex.ReleaseMutex();
        }
      }

      public void Close()
      {
        if (this.udpClient == null)
          return;
        this.udpClient.Close();
        this.udpClient = (UdpClient) null;
      }

      public bool Bind(int port, IPAddress ipaddress)
      {
        try
        {
          this.Close();
          this.udpClient = new UdpClient(new IPEndPoint(ipaddress, port));
          this.localPort = port;
          return true;
        }
        catch
        {
          return false;
        }
      }

      public bool Bind(int port) => this.Bind(port, IPAddress.Any);

      public void BeginReceive()
      {
        try
        {
          this.udpClient.BeginReceive(new System.AsyncCallback(this.AsyncCallback), (object) this);
        }
        catch
        {
        }
      }

      public int Send(byte[] dgram, int bytes) => this.udpClient.Send(dgram, bytes);

      public int Send(byte[] dgram, int bytes, IPEndPoint endPoint)
      {
        return this.udpClient.Send(dgram, bytes, endPoint);
      }

      public int Send(byte[] dgram, int bytes, string hostname, int port)
      {
        return this.udpClient.Send(dgram, bytes, hostname, port);
      }

      public int Send(UdpCommand udpCommand, IPEndPoint endPoint)
      {
        byte[] cmdBytes = udpCommand.GetCmdBytes();
        return this.Send(cmdBytes, cmdBytes.Length, endPoint);
      }

      public int Send(UdpCommand udpCommand, string hostname, int port)
      {
        byte[] cmdBytes = udpCommand.GetCmdBytes();
        return this.Send(cmdBytes, cmdBytes.Length, hostname, port);
      }

      private void AsyncCallback(IAsyncResult ar)
      {
        try
        {
          IPEndPoint remoteEP = (IPEndPoint) null;
          byte[] buffer = this.udpClient.EndReceive(ar, ref remoteEP);
          if (this.recv_Click != null)
          {
            try
            {
              this.recv_Click(buffer, remoteEP);
            }
            catch
            {
            }
          }
          UdpCommand udpCommand = UdpCommand.Analysis(buffer);
          if (udpCommand == null)
            return;
          udpCommand.remoteEP = remoteEP;
          this.AddCommand(udpCommand);
          if (this.Command_Recv == null)
            return;
          try
          {
            this.Command_Recv(udpCommand);
          }
          catch
          {
          }
        }
        catch (Exception ex)
        {
        }
        finally
        {
          this.BeginReceive();
        }
      }

      public delegate void Recv_Click(byte[] buffer, IPEndPoint remoteEP);

      public delegate void Recv_Command_Click(UdpCommand command);
    }
}
