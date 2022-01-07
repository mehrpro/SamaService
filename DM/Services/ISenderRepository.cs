using System;
using System.Collections.Generic;
using System.Data.Entity;
using TSMSLIB_TLB;
using System.Linq;
using System.Threading.Tasks;
using DM.Infrastructure;
using DM.Models;

namespace DM.Services
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
        private readonly IDbSet<Student> _student;
        private readonly IDbSet<BirthRegister> _birthRegisters;
        private readonly TSMS_Tooba _tooba;

        public SenderRepository(IUnitOfWork unitOfWork, ILoggerRepository loggerRepository)
        {
            _unitOfWork = unitOfWork;
            _loggerRepository = loggerRepository;
            _tagRecorders = _unitOfWork.Set<TagRecorder>();
            _student = _unitOfWork.Set<Student>();
            _birthRegisters = _unitOfWork.Set<BirthRegister>();
            _tooba = new TSMS_Tooba();
        }

        public void SendBrith(long mobile, string fullName, int id)
        {
            try
            {
                var qryStudent = _student.ToList();
                foreach (var selectStudent in qryStudent)
                {
                    if (selectStudent.BrithDate.Date.Month == DateTime.Now.Month &&
                        selectStudent.BrithDate.Day == DateTime.Now.Day)
                    {
                        var res = _birthRegisters.Where(x => x.StudentID_FK == selectStudent.ID);
                        if (!res.Any(x =>
                            x.Registered.Date.Year == DateTime.Now.Year &&
                            x.Registered.Date.Month == DateTime.Now.Month))
                        {
                            _tooba.UserName = Public_Class.SMS_Username;
                            _tooba.Password = Public_Class.SMS_Password;
                            _tooba.LibKey = Public_Class.SMS_LibKey;
                            _tooba.ProxyPassword = null;
                            _tooba.ProxyPort = 0;
                            _tooba.ProxyServer = null;
                            _tooba.ProxyUserName = null;
                            string vs = "09186620474";
                            string number = "";
                            int credit = 0;
                            int usedCredit = 0;
                            string lastDate = "";

                            var resultUserInfo = _tooba.GetUserInfo(ref number, ref credit, ref usedCredit, ref lastDate);
                            if (credit < 12000 && !Public_Class.Send1200)
                            {
                                string creditMessage = $"اعتبار سامانه پیامکی {credit} ریال می باشد سرویس متوقف گردید";
                                var resultSend = _tooba.SendSMS(vs, creditMessage);
                                if (resultSend > 0)
                                {
                                    _loggerRepository.WriteMessageSenderLog($"Send Credit 1000 to PhoneNumber :  {vs} , {resultSend}");
                                    Public_Class.Send1200 = true;
                                }
                                else
                                {
                                    Public_Class.Send1200 = false;
                                }
                                //return false;
                            }
                            else
                            {
                                if (credit.Between(12001, 50000) && !Public_Class.Send5000)
                                {
                                    string creditMessage = $"اعتبار سامانه پیامکی {credit} ریال می باشد";
                                    var resultCredit = _tooba.SendSMS(vs, creditMessage);

                                    if (resultCredit > 0)
                                    {
                                        _loggerRepository.WriteMessageSenderLog($"Send Credit 5000 to PhoneNumber :  {vs}  , {resultCredit}");
                                        Public_Class.Send5000 = true;
                                    }
                                }
                                if (credit > 50000)
                                {
                                    Public_Class.Send1200 = Public_Class.Send5000 = false;
                                }
                                string to = mobile.ToString("00000000000");

                                string content = $"{fullName} تولدت مبارک\r\n دبستان دخترانه سما بیجار";
                                var result = _tooba.SendSMS(to, content);

                                if (result > 0)
                                {
                                    var newInsert = new BirthRegister();
                                    newInsert.StudentID_FK = id;
                                    newInsert.Registered = DateTime.Now;
                                    _birthRegisters.Add(newInsert);
                                    _unitOfWork.SaveChanges();
                                    _loggerRepository.WriteMessageSenderLog($"Send Tavalood {mobile} , {result}");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                _loggerRepository.WriteErrorLog(e, "خطا در ثبت تبریک تولد");
            }
        }

        public void SendInput(long mobile, string fullName, string inDate, int id)
        {
            try
            {
                _tooba.UserName = Public_Class.SMS_Username;
                _tooba.Password = Public_Class.SMS_Password;
                _tooba.LibKey = Public_Class.SMS_LibKey;
                _tooba.ProxyPassword = null;
                _tooba.ProxyPort = 0;
                _tooba.ProxyServer = null;
                _tooba.ProxyUserName = null;
                string vs = "09186620474";
                string number = "";
                int credit = 0;
                int usedCredit = 0;
                string lastDate = "";

                var resultUserInfo = _tooba.GetUserInfo(ref number, ref credit, ref usedCredit, ref lastDate);
                if (credit < 12000 && !Public_Class.Send1200)
                {
                    string creditMessage = $"اعتبار سامانه پیامکی {credit} ریال می باشد سرویس متوقف گردید";
                    var resultSend = _tooba.SendSMS(vs, creditMessage);
                    if (resultSend > 0)
                    {
                        _loggerRepository.WriteMessageSenderLog($"Send Credit 1000 to PhoneNumber :  {vs} , {resultSend}");
                        Public_Class.Send1200 = true;
                    }
                    else
                    {
                        Public_Class.Send1200 = false;
                    }
                    //return false;
                }
                else
                {
                    if (credit.Between(12001, 50000) && !Public_Class.Send5000)
                    {
                        string creditMessage = $"اعتبار سامانه پیامکی {credit} ریال می باشد";
                        var resultCredit = _tooba.SendSMS(vs, creditMessage);

                        if (resultCredit > 0)
                        {
                            _loggerRepository.WriteMessageSenderLog($"Send Credit 5000 to PhoneNumber :  {vs}  , {resultCredit}");
                            Public_Class.Send5000 = true;
                        }
                    }
                    if (credit > 50000)
                    {
                        Public_Class.Send1200 = Public_Class.Send5000 = false;
                    }
                    string to = mobile.ToString("00000000000");
                    string content = $"ورود دانش آموز {fullName} در تاریخ {inDate} ثبت گردید";
                    var result = _tooba.SendSMS(to, content);
                    if (result > 0)
                    {
                        var find = _tagRecorders.Find(id);
                        find.SMS = true;
                        _unitOfWork.SaveChanges();
                        _loggerRepository.WriteMessageSenderLog($"Send Input {mobile},{result}");
                    }
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
                _tooba.UserName = Public_Class.SMS_Username;
                _tooba.Password = Public_Class.SMS_Password;
                _tooba.LibKey = Public_Class.SMS_LibKey;
                _tooba.ProxyPassword = null;
                _tooba.ProxyPort = 0;
                _tooba.ProxyServer = null;
                _tooba.ProxyUserName = null;
                string vs = "09186620474";
                string number = "";
                int credit = 0;
                int usedCredit = 0;
                string lastDate = "";

                var resultUserInfo = _tooba.GetUserInfo(ref number, ref credit, ref usedCredit, ref lastDate);
                if (credit < 12000 && !Public_Class.Send1200)
                {
                    string creditMessage = $"اعتبار سامانه پیامکی {credit} ریال می باشد سرویس متوقف گردید";
                    var resultSend = _tooba.SendSMS(vs, creditMessage);
                    if (resultSend > 0)
                    {
                        _loggerRepository.WriteMessageSenderLog($"Send Credit 1000 to PhoneNumber :  {vs} , {resultSend}");
                        Public_Class.Send1200 = true;
                    }
                    else
                    {
                        Public_Class.Send1200 = false;
                    }
                    //return false;
                }
                else
                {
                    if (credit.Between(12001, 50000) && !Public_Class.Send5000)
                    {
                        string creditMessage = $"اعتبار سامانه پیامکی {credit} ریال می باشد";
                        var resultCredit = _tooba.SendSMS(vs, creditMessage);

                        if (resultCredit > 0)
                        {
                            _loggerRepository.WriteMessageSenderLog($"Send Credit 5000 to PhoneNumber :  {vs}  , {resultCredit}");
                            Public_Class.Send5000 = true;
                        }
                    }
                    if (credit > 50000)
                    {
                        Public_Class.Send1200 = Public_Class.Send5000 = false;
                    }
                    string to = mobile.ToString("00000000000");
                    string content = $"خروج دانش آموز {fullName} در تاریخ {inDate} ثبت گردید";
                    var result = _tooba.SendSMS(to, content);
                    if (result > 0)
                    {
                        var find = _tagRecorders.Find(id);
                        find.SMS = true;
                        _unitOfWork.SaveChanges();
                        _loggerRepository.WriteMessageSenderLog($"Send Input {mobile},{result}");
                    }
                }
            }
            catch (Exception e)
            {
                _loggerRepository.WriteErrorLog(e, "SendInput");
            }
        }
    }
}
