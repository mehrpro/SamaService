using System;
using System.ServiceProcess;
using System.Timers;
using DM.Infrastructure;
using StructureMap;
using DM.Services;


namespace SamaService
{
    public partial class SamaService2 : ServiceBase
    {

        private SMSSenderProcess sendserv;
        private DatabaseTransFormerProcess dataTrans;
        private StructureMap.Container container;
        private Timer _timer5;
        private bool _sendBrith;
        public SamaService2()
        {
            InitializeComponent();

            container = new Container(new TypeRegistery());


        }

        protected override void OnStart(string[] args)
        {

            DM.Public_Class.Send10000 = DM.Public_Class.Send1200 = DM.Public_Class.Send5000 = false;
            try
            {
                _timer5 = new Timer();
                _timer5.Interval = 3000; // every 5 Secs
                _timer5.Elapsed += new ElapsedEventHandler(this.timer5_Tick);
                _timer5.Enabled = true;
                Logger.WriteMessageLog(" Start Service");
                Logger.WriteMessageLog(" Start SMS Service");
            }
            catch
            {
                Logger.WriteMessageLog("StartError");
            }


        }

        private void timer5_Tick(object sender, ElapsedEventArgs e)
        {
            Logger.WriteMessageLog("Timer1");
            sendserv = container.GetInstance<SMSSenderProcess>();
            Logger.WriteMessageLog("Timer2");
            dataTrans = container.GetInstance<DatabaseTransFormerProcess>();
            Logger.WriteMessageLog("Timer3");
            dataTrans.TransformDataBase();
            Logger.WriteMessageLog("Timer4");
            sendserv.UpdateTagID();
            Logger.WriteMessageLog("Timer5");
            sendserv.NewTags();
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
