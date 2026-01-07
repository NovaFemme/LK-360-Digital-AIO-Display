using System;
using System.Management;
using System.Threading;

namespace HardwareHelp
{
    public class UsbNotificationHelp : IDisposable
    {
      private static ManagementEventWatcher watcher = (ManagementEventWatcher) null;
      private static bool usbFlag = false;
      private static WaitCallback usbNotification = (WaitCallback) null;
      private static object lockCallBack = new object();
      public UsbNotificationHelp.CallBack_UsbNotification callBack;

      private static void AddCallBack(WaitCallback callback)
      {
        try
        {
          lock (UsbNotificationHelp.lockCallBack)
          {
            if (UsbNotificationHelp.watcher == null)
              UsbNotificationHelp.UsbNotification();
            UsbNotificationHelp.usbNotification += callback;
          }
        }
        catch
        {
        }
      }

      private static void RemoveCallBack(WaitCallback callback)
      {
        try
        {
          lock (UsbNotificationHelp.lockCallBack)
          {
            if (UsbNotificationHelp.usbNotification == null)
              return;
            UsbNotificationHelp.usbNotification -= callback;
          }
        }
        catch
        {
        }
      }

      private static void UsbNotification_CallBack(object obj)
      {
        try
        {
          lock (UsbNotificationHelp.lockCallBack)
          {
            if (!UsbNotificationHelp.usbFlag)
              return;
          }
          Thread.Sleep(100);
          try
          {
            WaitCallback usbNotification = UsbNotificationHelp.usbNotification;
            if (usbNotification != null)
              usbNotification((object) null);
          }
          catch
          {
          }
          lock (UsbNotificationHelp.lockCallBack)
            UsbNotificationHelp.usbFlag = false;
        }
        catch (Exception ex)
        {
        }
      }

      private static void UsbNotification()
      {
        try
        {
          UsbNotificationHelp.usbFlag = false;
          if (UsbNotificationHelp.watcher != null)
            return;
          UsbNotificationHelp.watcher = new ManagementEventWatcher();
          WqlEventQuery wqlEventQuery = new WqlEventQuery("SELECT * FROM Win32_DeviceChangeEvent  WHERE EventType = 2 or EventType = 3");
          UsbNotificationHelp.watcher.EventArrived += (EventArrivedEventHandler) ((s, e) =>
          {
            lock (UsbNotificationHelp.lockCallBack)
            {
              if (UsbNotificationHelp.usbFlag || UsbNotificationHelp.usbNotification == null)
                return;
              UsbNotificationHelp.usbFlag = true;
              ThreadPool.QueueUserWorkItem(new WaitCallback(UsbNotificationHelp.UsbNotification_CallBack));
            }
          });
          UsbNotificationHelp.watcher.Query = (EventQuery) wqlEventQuery;
          UsbNotificationHelp.watcher.Start();
        }
        catch (Exception ex)
        {
        }
      }

      public UsbNotificationHelp()
      {
        UsbNotificationHelp.AddCallBack(new WaitCallback(this.Notification_CallBack));
      }

      public UsbNotificationHelp(
        UsbNotificationHelp.CallBack_UsbNotification callBack)
      {
        this.callBack = callBack;
        UsbNotificationHelp.AddCallBack(new WaitCallback(this.Notification_CallBack));
      }

      ~UsbNotificationHelp() => this.Dispose();

      public void Dispose()
      {
        UsbNotificationHelp.RemoveCallBack(new WaitCallback(this.Notification_CallBack));
      }

      private void Notification_CallBack(object obj)
      {
        try
        {
          UsbNotificationHelp.CallBack_UsbNotification callBack = this.callBack;
          if (callBack == null)
            return;
          callBack();
        }
        catch
        {
        }
      }

      private delegate void EventUsbNotification();

      public delegate void CallBack_UsbNotification();
    }
}
