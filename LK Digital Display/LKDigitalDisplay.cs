using System;
using System.ComponentModel;
using System.Reflection;
using System.ServiceProcess;
using XKWLib;

namespace LK_Digital_Display
{
    public class LKDigitalDisplay : ServiceBase
    {
      private string serverName = Assembly.GetExecutingAssembly().GetName().Name;
      private XKWLibHelp xkw;
      private IContainer components;

      //public LKDigitalDisplay() => this.InitializeComponent();

      protected override void OnStart(string[] args)
      {
            Console.WriteLine("Service Started.");
            try
            {
              if (this.xkw != null)
                return;
              this.xkw = new XKWLibHelp();
              this.xkw.localPort = 8896;
              if (!this.xkw.Start(this.serverName))
                throw new Exception("Initialization failed");
            }
            catch (Exception ex)
            {
              Log.IsLog = true;
              Log.LogPath = AppDomain.CurrentDomain.BaseDirectory + "log.txt";
              Log.WriteLog($"{MethodBase.GetCurrentMethod().DeclaringType.FullName}.{MethodBase.GetCurrentMethod().Name}:\r\n\t{ex.ToString()}");
              throw ex;
            }
      }

      protected override void OnStop()
      {
            Console.WriteLine("Service Stopped.");
            try
            {
              if (this.xkw == null)
                return;
              this.xkw.Stop();
            }
            catch
            {

            }
      }

      protected override void Dispose(bool disposing)
      {
        if (disposing && this.components != null)
          this.components.Dispose();
        base.Dispose(disposing);
      }

        public void RunAsConsole(string[] args)
        {
            OnStart(args);
            this.components = (IContainer)new System.ComponentModel.Container();
            Console.WriteLine("Press any key to stop...");
            Console.ReadLine(); // Keep the app running
            //OnStop();
        }

      //  private void InitializeComponent()
      //{
      //  this.components = (IContainer) new System.ComponentModel.Container();
      //  this.ServiceName = "Service1";
      //}
    }
}
