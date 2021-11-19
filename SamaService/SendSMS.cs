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
                string[] vs = new[] { "09186620474" };
                var resultUsernfo = sms.UserInfo("iaubijar", "M4228056");
                //چک کردن اعتبار زیر 1200 تومن و توقف سرویس ارسال
                if (resultUsernfo[0].credit < 12000 && !PublicClass.Send1200)
                {
                    string[] creditMessage = { $"اعتبار سامانه پیامکی {resultUsernfo[0].credit} ریال می باشد سرویس متوقف گردید" };
                    var resultSend = sms.sendSms("iaubijar", "M4228056", from, vs, creditMessage, new string[] { }, "");
                    if (resultSend[0] > 0)
                    {
                        Logger.WriteMessageSenderLog($"Send Credit 1000 to PhoneNumber :  {vs} , {resultSend[0]}");
                        PublicClass.Send1200 = true;
                    }
                    return;
                }
                if (resultUsernfo[0].credit.Between(12001, 50000) && !PublicClass.Send5000)
                {
                    string[] creditMessage = { $"اعتبار سامانه پیامکی {resultUsernfo[0].credit} ریال می باشد" };
                    var resultCredit = sms.sendSms("iaubijar", "M4228056", from, vs, creditMessage, new string[] { }, "");
                    if (resultCredit[0] > 0)
                    {
                        Logger.WriteMessageSenderLog($"Send Credit 5000 to PhoneNumber :  {vs}  , {resultCredit[0]}");
                        PublicClass.Send5000 = true;
                    }
                }
                if (resultUsernfo[0].credit > 50000)
                {
                    PublicClass.Send1200 = PublicClass.Send5000 = false;
                }
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
                string[] vs = new[] { "09186620474" };
                var resultUsernfo = sms.UserInfo("iaubijar", "M4228056");
                //چک کردن اعتبار زیر 1200 تومن و توقف سرویس ارسال
                if (resultUsernfo[0].credit < 12000 && !PublicClass.Send1200)
                {
                    string[] creditMessage = { $"اعتبار سامانه پیامکی {resultUsernfo[0].credit} ریال می باشد سرویس متوقف گردید" };
                    var resultSend = sms.sendSms("iaubijar", "M4228056", from, vs, creditMessage, new string[] { }, "");
                    if (resultSend[0] > 0)
                    {
                        Logger.WriteMessageSenderLog($"Send Credit 1000 to PhoneNumber :  {vs} , {resultSend[0]}");
                        PublicClass.Send1200 = true;
                    }
                    return;
                }
                if (resultUsernfo[0].credit.Between(12001, 50000) && !PublicClass.Send5000)
                {
                    string[] creditMessage = { $"اعتبار سامانه پیامکی {resultUsernfo[0].credit} ریال می باشد" };
                    var resultCredit = sms.sendSms("iaubijar", "M4228056", from, vs, creditMessage, new string[] { }, "");
                    if (resultCredit[0] > 0)
                    {
                        Logger.WriteMessageSenderLog($"Send Credit 5000 to PhoneNumber :  {vs}  , {resultCredit[0]}");
                        PublicClass.Send5000 = true;
                    }
                }
                if (resultUsernfo[0].credit > 50000)
                {
                    PublicClass.Send1200 = PublicClass.Send5000 = false;
                }
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


        public static void SendBrithDay(long mobile, string fullName, int id)
        {
            try
            {
                var sms = new tsmsServiceClient();
                string[] from = new[] { "30007227001374" };
                string[] vs = new[] { "09186620474" };
                var resultUsernfo = sms.UserInfo("iaubijar", "M4228056");
                //چک کردن اعتبار زیر 1200 تومن و توقف سرویس ارسال
                if (resultUsernfo[0].credit < 12000 && !PublicClass.Send1200)
                {
                    string[] creditMessage = { $"اعتبار سامانه پیامکی {resultUsernfo[0].credit} ریال می باشد سرویس متوقف گردید" };
                    var resultSend = sms.sendSms("iaubijar", "M4228056", from, vs, creditMessage, new string[] { }, "");
                    if (resultSend[0] > 0)
                    {
                        Logger.WriteMessageSenderLog($"Send Credit 1000 to PhoneNumber :  {vs} , {resultSend[0]}");
                        PublicClass.Send1200 = true;
                    }
                    return;
                }
                if (resultUsernfo[0].credit.Between(12001, 50000) && !PublicClass.Send5000)
                {
                    string[] creditMessage = { $"اعتبار سامانه پیامکی {resultUsernfo[0].credit} ریال می باشد" };
                    var resultCredit = sms.sendSms("iaubijar", "M4228056", from, vs, creditMessage, new string[] { }, "");
                    if (resultCredit[0] > 0)
                    {
                        Logger.WriteMessageSenderLog($"Send Credit 5000 to PhoneNumber :  {vs}  , {resultCredit[0]}");
                        PublicClass.Send5000 = true;
                    }
                }
                if (resultUsernfo[0].credit > 50000)
                {
                    PublicClass.Send1200 = PublicClass.Send5000 = false;
                }
                string[] to = new[] { mobile.ToString("00000000000") };
                string[] content = { $"{fullName} تولدت مبارک\r\n دبستان دخترانه سما بیجار" };
                var result = sms.sendSms("iaubijar", "M4228056", from, to, content, new string[] { }, "");

                if (result[0] > 0)
                {
                    using (var dbx = new ApplicationDbContext())
                    {
                        dbx.BirthRegisters.Add(new BirthRegister() { StudentID_FK = id, Registered = DateTime.Today });
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


