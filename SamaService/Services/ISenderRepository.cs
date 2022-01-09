using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using SamaService.Infrastructure;
using SamaService.Models;
using SamaService.ServiceReference1;

namespace SamaService.Services
{
    public interface ISenderRepository
    {
        void SendInput(long mobile, string fullName, string inDate, int id);
        void SendOutput(long mobile, string fullName, string inDate, int id);
        void SendBrith(long mobile, string fullName, int id);
    }


    public class SenderRepository : ISenderRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerRepository _loggerRepository;
        private readonly IDbSet<TagRecorder> _tagRecorders;
        private readonly IDbSet<BirthRegister> _birthRegisters;
        private IUnitOfWorkClass _unitOfWorkClass;

        public SenderRepository(IUnitOfWork unitOfWork, ILoggerRepository loggerRepository, IUnitOfWorkClass unitOfWorkClass)
        {
            _unitOfWork = unitOfWork;
            _loggerRepository = loggerRepository;
            _tagRecorders = _unitOfWork.Set<TagRecorder>();
            _birthRegisters = _unitOfWork.Set<BirthRegister>();
            _unitOfWorkClass = unitOfWorkClass;
        }

        public void SendBrith(long mobile, string fullName, int id)
        {
            try
            {
                //var sms = new tsmsServiceClient();
                string[] from = new[] { "30007227001374" };
                string[] vs = new[] { "09186620474" };
                var resultUsernfo = _unitOfWorkClass.SMSClient.UserInfo("iaubijar", "M4228056");
                //چک کردن اعتبار زیر 1200 تومن و توقف سرویس ارسال
                if (resultUsernfo[0].credit < 12000 && !PublicStaticClass.Send1200)
                {
                    string[] creditMessage = { $"اعتبار سامانه پیامکی {resultUsernfo[0].credit} ریال می باشد سرویس متوقف گردید" };
                    var resultSend = _unitOfWorkClass.SMSClient.sendSms("iaubijar", "M4228056", from, vs, creditMessage, new string[] { }, "");
                    if (resultSend[0] > 0)
                    {
                        _loggerRepository.WriteMessageSenderLog($"Send Credit 1000 to PhoneNumber :  {vs} , {resultSend[0]}");
                        PublicStaticClass.Send1200 = true;
                    }
                    return;
                }
                if (resultUsernfo[0].credit.Between(12001, 50000) && !PublicStaticClass.Send5000)
                {
                    string[] creditMessage = { $"اعتبار سامانه پیامکی {resultUsernfo[0].credit} ریال می باشد" };
                    var resultCredit = _unitOfWorkClass.SMSClient.sendSms("iaubijar", "M4228056", from, vs, creditMessage, new string[] { }, "");
                    if (resultCredit[0] > 0)
                    {
                        _loggerRepository.WriteMessageSenderLog($"Send Credit 5000 to PhoneNumber :  {vs}  , {resultCredit[0]}");
                        PublicStaticClass.Send5000 = true;
                    }
                }
                if (resultUsernfo[0].credit > 50000)
                {
                    PublicStaticClass.Send1200 = PublicStaticClass.Send5000 = false;
                }
                string[] to = new[] { mobile.ToString("00000000000") };
                string[] content = { $"{fullName} تولدت مبارک\r\n دبستان دخترانه سما بیجار" };
                var result = _unitOfWorkClass.SMSClient.sendSms("iaubijar", "M4228056", from, to, content, new string[] { }, "");

                if (result[0] > 0)
                {

                    _birthRegisters.Add(new BirthRegister() { StudentID_FK = id, Registered = DateTime.Today });
                    _unitOfWork.SaveChanges();
                    _loggerRepository.WriteMessageSenderLog($"Send BirthDay {mobile} - {result[0]}");

                }
            }
            catch (Exception e)
            {
                _loggerRepository.WriteErrorLog(e, "SendOutput");
            }

        }

        public void SendInput(long mobile, string fullName, string inDate, int id)
        {
            try
            {
                //var sms = new tsmsServiceClient();
                string[] from = new[] { "30007227001374" };
                string[] vs = new[] { "09186620474" };
                var resultUsernfo = _unitOfWorkClass.SMSClient.UserInfo("iaubijar", "M4228056");
                //چک کردن اعتبار زیر 1200 تومن و توقف سرویس ارسال
                if (resultUsernfo[0].credit < 12000 && !PublicStaticClass.Send1200)
                {
                    string[] creditMessage = { $"اعتبار سامانه پیامکی {resultUsernfo[0].credit} ریال می باشد سرویس متوقف گردید" };
                    var resultSend = _unitOfWorkClass.SMSClient.sendSms("iaubijar", "M4228056", from, vs, creditMessage, new string[] { }, "");
                    if (resultSend[0] > 0)
                    {
                        _loggerRepository.WriteMessageSenderLog($"Send Credit 1000 to PhoneNumber :  {vs} , {resultSend[0]}");
                        PublicStaticClass.Send1200 = true;
                    }
                    return;
                }
                if (resultUsernfo[0].credit.Between(12001, 50000) && !PublicStaticClass.Send5000)
                {
                    string[] creditMessage = { $"اعتبار سامانه پیامکی {resultUsernfo[0].credit} ریال می باشد" };
                    var resultCredit = _unitOfWorkClass.SMSClient.sendSms("iaubijar", "M4228056", from, vs, creditMessage, new string[] { }, "");
                    if (resultCredit[0] > 0)
                    {
                        _loggerRepository.WriteMessageSenderLog($"Send Credit 5000 to PhoneNumber :  {vs}  , {resultCredit[0]}");
                        PublicStaticClass.Send5000 = true;
                    }
                }
                if (resultUsernfo[0].credit > 50000)
                {
                    PublicStaticClass.Send1200 = PublicStaticClass.Send5000 = false;
                }
                string[] to = new[] { mobile.ToString("00000000000") };
                string[] content = { $"ورود دانش آموز {fullName} در تاریخ {inDate} ثبت گردید" };
                var result = _unitOfWorkClass.SMSClient.sendSms("iaubijar", "M4228056", from, to, content, new string[] { }, "");
                if (result[0] > 0)
                {

                    var find = _tagRecorders.Find(id);
                    find.SMS = true;
                    _unitOfWork.SaveChanges();
                    _loggerRepository.WriteMessageSenderLog($"Send Input {mobile} - {result[0]}");

                }
            }
            catch (Exception e)
            {
                _loggerRepository.WriteErrorLog(e, "SendInput");
            }
        }

        public void SendOutput(long mobile, string fullName, string inDate, int id)
        {
            try
            {
                // var sms = new tsmsServiceClient();
                string[] from = new[] { "30007227001374" };
                string[] vs = new[] { "09186620474" };
                var resultUsernfo = _unitOfWorkClass.SMSClient.UserInfo("iaubijar", "M4228056");
                //چک کردن اعتبار زیر 1200 تومن و توقف سرویس ارسال
                if (resultUsernfo[0].credit < 12000 && !PublicStaticClass.Send1200)
                {
                    string[] creditMessage = { $"اعتبار سامانه پیامکی {resultUsernfo[0].credit} ریال می باشد سرویس متوقف گردید" };
                    var resultSend = _unitOfWorkClass.SMSClient.sendSms("iaubijar", "M4228056", from, vs, creditMessage, new string[] { }, "");
                    if (resultSend[0] > 0)
                    {
                        _loggerRepository.WriteMessageSenderLog($"Send Credit 1000 to PhoneNumber :  {vs} , {resultSend[0]}");
                        PublicStaticClass.Send1200 = true;
                    }
                    return;
                }
                if (resultUsernfo[0].credit.Between(12001, 50000) && !PublicStaticClass.Send5000)
                {
                    string[] creditMessage = { $"اعتبار سامانه پیامکی {resultUsernfo[0].credit} ریال می باشد" };
                    var resultCredit = _unitOfWorkClass.SMSClient.sendSms("iaubijar", "M4228056", from, vs, creditMessage, new string[] { }, "");
                    if (resultCredit[0] > 0)
                    {
                        _loggerRepository.WriteMessageSenderLog($"Send Credit 5000 to PhoneNumber :  {vs}  , {resultCredit[0]}");
                        PublicStaticClass.Send5000 = true;
                    }
                }
                if (resultUsernfo[0].credit > 50000)
                {
                    PublicStaticClass.Send1200 = PublicStaticClass.Send5000 = false;
                }
                string[] to = new[] { mobile.ToString("00000000000") };
                string[] content = { $"خروج دانش آموز {fullName} در تاریخ {inDate} ثبت گردید" };
                var result = _unitOfWorkClass.SMSClient.sendSms("iaubijar", "M4228056", from, to, content, new string[] { }, "");

                if (result[0] > 0)
                {

                    var find = _tagRecorders.Find(id);
                    find.SMS = true;
                    _unitOfWork.SaveChanges();
                    _loggerRepository.WriteMessageSenderLog($"Send Output {mobile} - {result[0]}");

                }
            }
            catch (Exception e)
            {
                _loggerRepository.WriteErrorLog(e, "SendOutput");
            }
        }
    }
}
