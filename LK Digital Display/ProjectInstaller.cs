using Microsoft.Win32;
using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.IO;
using System.ServiceProcess;
using System.Threading;

namespace LK_Digital_Display
{
    [RunInstaller(true)]
    public class ProjectInstaller : Installer
    {
      public static readonly string error1 = "Service installation failed, please restart the computer and reinstall";
      private static readonly string logPath = "d:\\temp\\install.txt";
      public static readonly string _serviceFile = "LK Digital Display.exe";
      public static readonly string _serviceName = "LKDigitalDisplay";
      private IContainer components;
      private ServiceProcessInstaller serviceProcessInstaller1;
      private ServiceInstaller serviceInstaller1;

      public static void AddLog(string txt)
      {
      }

      public static Exception LastException { get; private set; }

      public bool UpdatePath(string serviceName, string servicePath)
      {
        try
        {
          RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM").OpenSubKey("CurrentControlSet").OpenSubKey("Services").OpenSubKey(serviceName, true);
          if (registryKey != null && !new DirectoryInfo(registryKey.GetValue("ImagePath").ToString().Replace("\"", string.Empty)).FullName.Equals(new DirectoryInfo(servicePath).FullName, StringComparison.OrdinalIgnoreCase))
          {
            if (!this.Stop(serviceName))
              throw new Exception("服务停止失败");
            registryKey.SetValue("ImagePath", (object) servicePath);
          }
          return true;
        }
        catch (Exception ex)
        {
          ProjectInstaller.LastException = new Exception(ProjectInstaller.error1);
          return false;
        }
      }

      public string GetServiceFilePath(string serviceName = "")
      {
        string name = "SYSTEM\\CurrentControlSet\\Services\\" + serviceName;
        return new FileInfo(Registry.LocalMachine.OpenSubKey(name).GetValue("ImagePath").ToString().Replace("\"", string.Empty)).Directory.ToString();
      }

      public bool UnInstall(string serviceName)
      {
        try
        {
          if (this.GetService(serviceName) == null)
            return true;
          if (!this.Stop(serviceName))
            return false;
          string serviceFilePath = this.GetServiceFilePath(serviceName);
          ProjectInstaller.AddLog(serviceFilePath);
          using (AssemblyInstaller assemblyInstaller = new AssemblyInstaller())
          {
            assemblyInstaller.UseNewContext = true;
            assemblyInstaller.Path = Path.Combine(serviceFilePath, serviceName + ".exe");
            assemblyInstaller.Uninstall((IDictionary) null);
          }
          return true;
        }
        catch (Exception ex)
        {
          ProjectInstaller.AddLog(ex.Message);
          Console.WriteLine(ex.Message);
          ProjectInstaller.LastException = ex;
          return false;
        }
      }

      public bool IsInstall(string serviceName)
      {
        try
        {
          return this.GetService(serviceName) != null;
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.Message);
          ProjectInstaller.LastException = ex;
          return false;
        }
      }

      public ServiceController GetService(string serviceName)
      {
        try
        {
          foreach (ServiceController service in ServiceController.GetServices())
          {
            if (service.ServiceName.ToLower() == serviceName.ToLower())
              return service;
          }
          return (ServiceController) null;
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.Message);
          ProjectInstaller.LastException = ex;
          return (ServiceController) null;
        }
      }

      public bool Start(string serviceName)
      {
        try
        {
          using (ServiceController service = this.GetService(serviceName))
          {
            if (service == null)
              throw new Exception("服务未安装");
            if (service.Status != ServiceControllerStatus.StartPending)
            {
              if (service.Status != ServiceControllerStatus.Running)
                service.Start();
            }
          }
          return true;
        }
        catch (Exception ex)
        {
          ProjectInstaller.LastException = ex;
          Console.WriteLine(ex.Message);
          return false;
        }
      }

      public bool Stop(string serviceName, int delay = 60000)
      {
        try
        {
          int tickCount = Environment.TickCount;
          while (true)
          {
            ServiceController service = this.GetService(serviceName);
            if (service != null)
            {
              if (service.Status == ServiceControllerStatus.Running)
                service.Stop();
              else if (service.Status != ServiceControllerStatus.Stopped)
              {
                if (Environment.TickCount - tickCount > delay)
                  goto label_7;
              }
              else
                goto label_9;
              Thread.Sleep(100);
            }
            else
              break;
          }
          return true;
    label_7:
          return false;
    label_9:
          return true;
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.Message);
          ProjectInstaller.LastException = ex;
          return false;
        }
      }

      private string Assemblypath => Path.GetDirectoryName(this.Context.Parameters["assemblypath"]);

      private string ServicePath
      {
        get
        {
          return Path.Combine(Path.GetDirectoryName(this.Context.Parameters["assemblypath"]), ProjectInstaller._serviceFile);
        }
      }

      public ProjectInstaller() => this.InitializeComponent();

      public override void Commit(IDictionary savedState)
      {
        try
        {
          ProjectInstaller.AddLog(nameof (Commit));
          this.UpdatePath(ProjectInstaller._serviceName, this.ServicePath);
          if (!this.Start(ProjectInstaller._serviceName))
            throw ProjectInstaller.LastException;
          try
          {
            base.Commit(savedState);
          }
          catch
          {
          }
        }
        catch (Exception ex)
        {
          ProjectInstaller.AddLog(nameof (Commit) + ex.Message);
        }
      }

      public override void Install(IDictionary stateSaver)
      {
        try
        {
          ProjectInstaller.AddLog(nameof (Install));
          if (this.GetService(ProjectInstaller._serviceName) != null)
          {
            ProjectInstaller.AddLog("停止服务,卸载服务");
            this.Stop(ProjectInstaller._serviceName);
            this.UnInstall(ProjectInstaller._serviceName);
          }
          base.Install(stateSaver);
        }
        catch (Exception ex)
        {
          ProjectInstaller.AddLog(nameof (Install) + ex.Message);
        }
      }

      public override void Uninstall(IDictionary savedState)
      {
        try
        {
          base.Uninstall(savedState);
        }
        catch (Exception ex)
        {
          ProjectInstaller.AddLog(nameof (Uninstall) + ex.Message);
        }
        this.UnInstall(ProjectInstaller._serviceName);
      }

      protected override void Dispose(bool disposing)
      {
        if (disposing && this.components != null)
          this.components.Dispose();
        base.Dispose(disposing);
      }

      private void InitializeComponent()
      {
        this.serviceProcessInstaller1 = new ServiceProcessInstaller();
        this.serviceInstaller1 = new ServiceInstaller();
        this.serviceProcessInstaller1.Account = ServiceAccount.LocalSystem;
        this.serviceProcessInstaller1.Password = (string) null;
        this.serviceProcessInstaller1.Username = (string) null;
        this.serviceInstaller1.Description = "LK Digital Display";
        this.serviceInstaller1.DisplayName = "LK Digital Display";
        this.serviceInstaller1.ServiceName = "LK Digital Display";
        this.serviceInstaller1.StartType = ServiceStartMode.Automatic;
        this.Installers.AddRange(new Installer[2]
        {
          (Installer) this.serviceProcessInstaller1,
          (Installer) this.serviceInstaller1
        });
      }
    }
}
