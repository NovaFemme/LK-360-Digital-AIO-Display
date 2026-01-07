using System;
using System.ServiceProcess;

namespace LK_Digital_Display
{
    internal static class Program
    {
      private static void Main(string[] args)
      {
            if (Environment.UserInteractive)
            {
                var service = new LKDigitalDisplay();
                service.RunAsConsole(args);
            }
            else
            {
                ServiceBase.Run(new ServiceBase[1]
                {
                  (ServiceBase) new LKDigitalDisplay()
                });
            }

            
      }
    }
}
