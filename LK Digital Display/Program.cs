using System;
using System.ServiceProcess;

namespace LK_Digital_Display
{
    internal static class Program
    {
      private static void Main(string[] args)
      {
            ServiceBase.Run(new ServiceBase[1]
            {
              (ServiceBase) new LKDigitalDisplay()
            });
      }
    }
}
