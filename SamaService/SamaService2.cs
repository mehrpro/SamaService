using System.ServiceProcess;


namespace SamaService
{
    public partial class SamaService2 : ServiceBase
    {

        public SamaService2()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {

        }

        protected override void OnStop()
        {
        }
    }
}
