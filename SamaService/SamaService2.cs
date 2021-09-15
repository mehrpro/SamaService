using System;
using System.ServiceProcess;
using System.Timers;


namespace SamaService
{
    public partial class SamaService2 : ServiceBase
    {
        private Timer timer5;
        private bool sendBrith;
        public SamaService2()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            timer5 = new Timer();
            timer5.Interval = 3000; // every 5 Secs
            timer5.Elapsed += new ElapsedEventHandler(this.timer5_Tick);
            timer5.Enabled = true;

            Logger.WriteMessageLog(" Start Service");
            Logger.WriteMessageLog(" Start SMS Service");
        }
        private void timer5_Tick(object sender, ElapsedEventArgs e)
        {

            DatabaseTransFormerProcess.TransformDataBase();// انتقال داده های بین دو سرور
            SMSSenderProcess.NewTags();
            SMSSenderProcess.UpdateTagID();
            SMSSenderProcess.SMSSender();// پردازش . ارسال پیامک ها
            if (DateTime.Now.Hour > 16 && !sendBrith)
            {
                sendBrith = true;
                SMSSenderProcess.SMSSenderBirthDay();
            }
            if (DateTime.Now.Hour == 1) sendBrith = false;

        }

        protected override void OnStop()
        {
            timer5.Enabled = false;
            Logger.WriteMessageLog("All Service Stopped");
        }
    }
}
