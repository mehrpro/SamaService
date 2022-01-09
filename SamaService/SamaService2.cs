using System;
using System.ServiceProcess;
using System.Timers;
using SamaService.Infrastructure;
using StructureMap;


namespace SamaService
{
    public partial class SamaService2 : ServiceBase
    {


        private Container _container;
        private Timer _timer5;
        private bool _sendBrith;
        public SamaService2()
        {
            InitializeComponent();

        }

        protected override void OnStart(string[] args)
        {

            PublicStaticClass.Send10000 = PublicStaticClass.Send1200 = PublicStaticClass.Send5000 = false;
            try
            {
                _timer5 = new Timer();
                _timer5.Interval = 3000; // every 5 Secs
                _timer5.Elapsed += new ElapsedEventHandler(this.timer5_Tick);
                _timer5.Enabled = true;
                _container = new Container(new SamaService.Infrastructure.TypeRegistery());
                Logger.WriteMessageLog(" Start Service ");
                Logger.WriteMessageLog(" Start SMS Service ");
            }
            catch
            {
                Logger.WriteMessageLog("StartError");
            }


        }

        private void timer5_Tick(object sender, ElapsedEventArgs e)
        {


            var newtsg = _container.GetInstance<Process.NewTags>();
            newtsg.Run();
            Logger.WriteMessageLog("NewTag");

            var updateid = _container.GetInstance<Process.UpdateTagID>();
            updateid.Run();
            Logger.WriteMessageLog("UpdateTagID");


            Logger.WriteMessageLog("CreateSMSSender");
            var sendserv = _container.GetInstance<SMSSenderProcess>();


            // Logger.WriteMessageLog("Timer2");
            var dataTrans = _container.GetInstance<DatabaseTransFormerProcess>();
            Logger.WriteMessageLog("Timer3");
            dataTrans.TransformDataBase();


            Logger.WriteMessageLog("Timer6");
            sendserv.SMSSender();
            Logger.WriteMessageLog("Timer7");
            if (DateTime.Now.Hour > 16 && !_sendBrith)
            {
                _sendBrith = true;
                sendserv.SMSSenderBirthDay();
            }
            if (DateTime.Now.Hour == 1) _sendBrith = false;
        }

        protected override void OnStop()
        {
            _timer5.Enabled = false;
            Logger.WriteMessageLog("All Service Stopped");
        }
    }
}
