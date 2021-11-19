using System.ServiceProcess;

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
            PublicClass.Send10000 = PublicClass.Send1200 = PublicClass.Send5000 = false;
            ServiceBase.Run(ServicesToRun);
        }
    }
}
