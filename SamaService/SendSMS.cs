using System;
using SamaService.Models;
using SamaService.ServiceReference1;

namespace SamaService
{

    public class SendSMS
    {

        public static void SendInput(long mobile, string fullName, string inDate, int id)
        {
            try
            {
                var sms = new tsmsServiceClient();
                string[] from = new[] { "30007227001374" };
                string[] to = new[] { mobile.ToString("00000000000") };
                string[] content = { $"ورود دانش آموز {fullName} در تاریخ {inDate} ثبت گردید" };
                var result = sms.sendSms("iaubijar", "M4228056", from, to, content, new string[] { }, "");

                if (result[0] > 0)
                {
                    using (var dbx = new ApplicationDbContext())
                    {
                        var find = dbx.TagRecorders.Find(id);
                        find.SMS = true;
                        var resultSave = dbx.SaveChanges();
                        Logger.WriteMessageSenderLog(Convert.ToBoolean(resultSave) ? $"Send Input {mobile}" : $"Not Send Input {mobile}");
                    }
                }
            }
            catch (Exception e)
            {
                Logger.WriteErrorLog(e, "SendInput");
            }
        }
        public static void SendOutput(long mobile, string fullName, string inDate, int id)
        {
            try
            {
                var sms = new tsmsServiceClient();
                string[] from = new[] { "30007227001374" };
                string[] to = new[] { mobile.ToString("00000000000") };
                string[] content = { $"خروج دانش آموز {fullName} در تاریخ {inDate} ثبت گردید" };
                var result = sms.sendSms("iaubijar", "M4228056", from, to, content, new string[] { }, "");

                if (result[0] > 0)
                {
                    using (var dbx = new ApplicationDbContext())
                    {
                        var find = dbx.TagRecorders.Find(id);
                        find.SMS = true;
                        var resultSave = dbx.SaveChanges();
                        Logger.WriteMessageSenderLog(Convert.ToBoolean(resultSave) ? $"Send Input {mobile}" : $"Not Send Input {mobile}");
                    }
                }
            }
            catch (Exception e)
            {
                Logger.WriteErrorLog(e, "SendOutput");
            }
        }
    }
}


