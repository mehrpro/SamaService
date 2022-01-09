using System.ServiceProcess;
using StructureMap;
using SamaService;
using System;
using SamaService.Services;
using System.Threading;

namespace SamaService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
                {
                   new SamaService2()
                };
            ServiceBase.Run(ServicesToRun);

        }

    }
}
