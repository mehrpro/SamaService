using System;
using System.Collections.Generic;
using System.Linq;
using DM.DTO;
using DM.Infrastructure;
using DM.Services;
using DM.Models;
//using SamaService.ServiceReference1;
namespace SamaService
{
    public class SMSSenderProcess
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStudentRepository _studentRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IStudentTagRepository _studentTagRepository;
        private readonly ITagRecorderRepository _tagRecorderRepository;
        private readonly IBirthRegisterRepository _birthRegisterRepository;
        private readonly ISenderRepository _senderRepository;
        private readonly ILoggerRepository _loggerRepository;
        //public StructureMap.Container SendContainer { get; set; }
        public SMSSenderProcess(IUnitOfWork unitOfWork,
            IStudentRepository studentRepository,
            ITagRepository tagRepository,
            IStudentTagRepository studentTagRepository,
            ITagRecorderRepository tagRecorderRepository,
            IBirthRegisterRepository birthRegisterRepository,
            ISenderRepository senderRepository
            , ILoggerRepository loggerRepository)
        {
            _unitOfWork = unitOfWork;
            _studentRepository = studentRepository;
            _tagRepository = tagRepository;
            _studentTagRepository = studentTagRepository;
            _tagRecorderRepository = tagRecorderRepository;
            _birthRegisterRepository = birthRegisterRepository;
            _senderRepository = senderRepository;
            _loggerRepository = loggerRepository;
        }

        #region NewTags
        /// <summary>
        /// یافتن تگ های جدید و افزودن به بانک اطلاعاتی
        /// </summary>
        public void NewTags()
        {

            try
            {
                _loggerRepository.WriteMessageLog("NewTag");
                var qry = _tagRecorderRepository.GetAllByCondition(x => x.Enables && x.SMS == false);

                var qryAfterRemoveDublicate = qry.Select(x => x.TagID).ToList().RemoveDuplicates();
                foreach (var item in qryAfterRemoveDublicate)
                {
                    var tag = _tagRepository.GetFirstOrDefualt(x => x.TagID_HEX == item);

                    if (tag == null)
                    {
                        var newTag = new TagDTO()
                        {
                            TagID_HEX = item,
                            CartView = HexToDecimal(item)
                        };
                        _tagRepository.Insert(newTag);
                        _unitOfWork.SaveChanges();
                        _loggerRepository.WriteMessageLog($"Save New TAG : {item} ViewLabel {HexToDecimal(item)}");
                    }
                }
            }
            catch (Exception e)
            {
                _loggerRepository.WriteErrorLog(e, "NewTags");
            }


        }
        #endregion
        #region HexToDecimal

        private string HexToDecimal(string tagHex)
        {
            _loggerRepository.WriteMessageLog("HexToDecimal");
            var hexValue = tagHex.Remove(0, 2).Remove(8);
            int decValue = int.Parse(hexValue, System.Globalization.NumberStyles.HexNumber);
            var str = decValue.ToString("0000000000");
            return str;
        }

        #endregion
        #region UpdateTagID

        /// <summary>
        /// حذف تگ های خام و بروزرسانی شناسه تگ ها
        /// </summary>
        public void UpdateTagID()
        {
            _loggerRepository.WriteMessageLog("UpdateTagID");
            var listRemove = new List<TagRecorder>();
            try
            {
                var qry = _tagRecorderRepository.GetAllByCondition(x => x.Enables && x.SMS == false && x.TagID_FK == null);
                foreach (var item in qry)
                {
                    var result = _studentTagRepository.GetFirstOrDefault(x => x.Tag.TagID_HEX == item.TagID && x.Enabled);
                    if (result == null)
                    {
                        listRemove.Add(item);
                    }
                    else
                    {
                        item.TagID_FK = result.TagID_FK;
                        item.StudentID_FK = result.StudentID_FK;
                    }
                }
                _unitOfWork.SaveChanges();
            }
            catch (Exception e)
            {
                _loggerRepository.WriteErrorLog(e, "UpdateTagID");

            }
            finally
            {
                try
                {
                    _loggerRepository.WriteMessageLog("Finally");
                    _tagRecorderRepository.DeleteRange(listRemove);
                    _unitOfWork.SaveChanges();
                }
                catch (Exception e)
                {
                    _loggerRepository.WriteErrorLog(e, "UpdateTagID Finally");
                }
            }
        }


        #endregion
        #region SMSSender

        /// <summary>
        /// پردازش و ارسال پیامک تردد
        /// </summary>
        public void SMSSender()
        {
            _loggerRepository.WriteMessageLog("SMSSender");
            //تمام ثبت های امروز
            var qryDayRecorder = _tagRecorderRepository.GetAllByCondition(x => x.SMS == false && x.Enables && x.StudentID_FK > 0 && x.TagID_FK > 0); // تمام ثبت های امروز
            _loggerRepository.WriteMessageLog("Sender1");
            foreach (var item in qryDayRecorder) // جدول بندی تردد ها بر اساس هر تگ که در عین حال فقط و فقط متعلق به یک دانش آموز است
            {
                var student = _studentRepository.GetById(Convert.ToInt32(item.StudentID_FK));
                try
                {
                    if (item.Type) //ورودی ها 
                    {
                        var dat = item.DateTimeRegister.AddSeconds(-120);
                        var finder = _tagRecorderRepository.GetAny(x =>
                        x.TagID_FK == item.TagID_FK &&
                        x.DateTimeRegister > dat &&
                        x.SMS &&
                        x.Type &&
                        x.Enables);
                        if (finder)
                        {
                            item.SMS = true;
                            item.Enables = false;
                            _unitOfWork.SaveChanges();
                        }
                        else
                            _senderRepository.SendInput(Convert.ToInt64(student.SMS), student.FullName,
                                item.DateTimeRegister.Convert_PersianCalender(), item.ID); //ارسال
                        _loggerRepository.WriteMessageLog("Sender2");
                    }
                    else
                    {
                        var dat = item.DateTimeRegister.AddSeconds(-120);
                        var finder = _tagRecorderRepository.GetAny(x =>
                        x.TagID_FK == item.TagID_FK &&
                        x.DateTimeRegister > dat &&
                        x.SMS &&
                        x.Type == false &&
                        x.Enables);
                        if (finder)
                        {
                            item.SMS = true;
                            item.Enables = false;
                            _unitOfWork.SaveChanges();
                        }
                        else
                            _senderRepository.SendOutput(Convert.ToInt64(student.SMS), student.FullName,
                                item.DateTimeRegister.Convert_PersianCalender(), item.ID); ; //ارسال
                    }
                }
                catch (Exception e)
                {
                    _loggerRepository.WriteErrorLog(e, "خطا در پردازش و ارسال پیامک تردد");
                }
            }


        }

        #endregion
        #region BirthDay

        public void SMSSenderBirthDay()
        {
            try
            {
                var qryStudent = _studentRepository.GetAll();
                foreach (var selectStudent in qryStudent)
                {
                    if (selectStudent.BrithDate.Date.Month == DateTime.Now.Month &&
                        selectStudent.BrithDate.Day == DateTime.Now.Day)
                    {
                        var res = _birthRegisterRepository.GetAllByCondition(x => x.StudentID_FK == selectStudent.ID);
                        if (!res.Any(x =>
                            x.Registered.Date.Year == DateTime.Now.Year &&
                            x.Registered.Date.Month == DateTime.Now.Month))
                        {
                            _senderRepository.SendBrith(Convert.ToInt64(selectStudent.SMS), selectStudent.FullName,
                                selectStudent.ID);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                _loggerRepository.WriteErrorLog(e, "خطا در ثبت تبریک تولد");
            }
        }

        #endregion
        //#region SendInput
        //private void SendInput(long mobile, string fullName, string inDate, int id)
        //{
        //    try
        //    {
        //        var sms = new tsmsServiceClient();
        //        string[] from = new[] { "30007227001374" };
        //        string[] vs = new[] { "09183777118" };
        //        var resultUsernfo = sms.UserInfo("iaubijar", "M4228056");

        //        if (resultUsernfo[0].credit < 12000 && !PublicClass.Send1200)
        //        {
        //            string[] creditMessage = { $"اعتبار سامانه پیامکی {resultUsernfo[0].credit} ریال می باشد سرویس متوقف گردید" };
        //            var resultSend = sms.sendSms("iaubijar", "M4228056", from, vs, creditMessage, new string[] { }, "");
        //            if (resultSend[0] > 0)
        //            {
        //                Logger.WriteMessageSenderLog($"Send Credit 1000 to PhoneNumber :  {vs} , {resultSend[0]}");
        //                PublicClass.Send1200 = true;
        //            }
        //            return;
        //        }
        //        else
        //        {
        //            if (resultUsernfo[0].credit.Between(12001, 50000) && !PublicClass.Send5000)
        //            {
        //                string[] creditMessage = { $"اعتبار سامانه پیامکی {resultUsernfo[0].credit} ریال می باشد" };
        //                var resultCredit = sms.sendSms("iaubijar", "M4228056", from, vs, creditMessage, new string[] { }, "");
        //                if (resultCredit[0] > 0)
        //                {
        //                    Logger.WriteMessageSenderLog($"Send Credit 5000 to PhoneNumber :  {vs}  , {resultCredit[0]}");
        //                    PublicClass.Send5000 = true;
        //                }
        //            }
        //            if (resultUsernfo[0].credit > 50000)
        //            {
        //                PublicClass.Send1200 = PublicClass.Send5000 = false;
        //            }
        //            string[] to = new[] { mobile.ToString("00000000000") };
        //            string[] content = { $"ورود دانش آموز {fullName} در تاریخ {inDate} ثبت گردید" };
        //            var result = sms.sendSms("iaubijar", "M4228056", from, to, content, new string[] { }, "");
        //            if (result[0] > 0)
        //            {
        //                var find = _tagRecorderRepository.GetById(id);
        //                find.SMS = true;
        //                _unitOfWork.SaveChanges();
        //                Logger.WriteMessageSenderLog($"Send Input {mobile},{result[0]}");
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Logger.WriteErrorLog(e, "SendInput");
        //    }
        //}
        //#endregion
        //#region SendOutput
        //private void SendOutput(long mobile, string fullName, string inDate, int id)
        //{
        //    try
        //    {
        //        var sms = new tsmsServiceClient();
        //        string[] from = new[] { "30007227001374" };
        //        string[] vs = new[] { "09186620474" };
        //        var resultUsernfo = sms.UserInfo("iaubijar", "M4228056");
        //        //چک کردن اعتبار زیر 1200 تومن و توقف سرویس ارسال
        //        if (resultUsernfo[0].credit < 12000 && !PublicClass.Send1200)
        //        {
        //            string[] creditMessage = { $"اعتبار سامانه پیامکی {resultUsernfo[0].credit} ریال می باشد سرویس متوقف گردید" };
        //            var resultSend = sms.sendSms("iaubijar", "M4228056", from, vs, creditMessage, new string[] { }, "");
        //            if (resultSend[0] > 0)
        //            {
        //                Logger.WriteMessageSenderLog($"Send Credit 1000 to PhoneNumber :  {vs} , {resultSend[0]}");
        //                PublicClass.Send1200 = true;
        //            }
        //            return;
        //        }
        //        else
        //        {
        //            if (resultUsernfo[0].credit.Between(12001, 50000) && !PublicClass.Send5000)
        //            {
        //                string[] creditMessage = { $"اعتبار سامانه پیامکی {resultUsernfo[0].credit} ریال می باشد" };
        //                var resultCredit = sms.sendSms("iaubijar", "M4228056", from, vs, creditMessage, new string[] { }, "");
        //                if (resultCredit[0] > 0)
        //                {
        //                    Logger.WriteMessageSenderLog($"Send Credit 5000 to PhoneNumber :  {vs}  , {resultCredit[0]}");
        //                    PublicClass.Send5000 = true;
        //                }
        //            }
        //            if (resultUsernfo[0].credit > 50000)
        //            {
        //                PublicClass.Send1200 = PublicClass.Send5000 = false;
        //            }
        //            string[] to = new[] { mobile.ToString("00000000000") };
        //            string[] content = { $"خروج دانش آموز {fullName} در تاریخ {inDate} ثبت گردید" };
        //            var result = sms.sendSms("iaubijar", "M4228056", from, to, content, new string[] { }, "");

        //            if (result[0] > 0)
        //            {

        //                var find = _tagRecorderRepository.GetById(id);
        //                find.SMS = true;
        //                _unitOfWork.SaveChanges();
        //                Logger.WriteMessageSenderLog($"Send Input {mobile},{result[0]}");

        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Logger.WriteErrorLog(e, "SendOutput");
        //    }
        //}
        //#endregion
        //#region SendBrithDay
        //private void SendBrithDay(long mobile, string fullName, int id)
        //{
        //    try
        //    {
        //        var sms = new tsmsServiceClient();
        //        string[] from = new[] { "30007227001374" };
        //        string[] vs = new[] { "09186620474" };
        //        var resultUsernfo = sms.UserInfo("iaubijar", "M4228056");
        //        //چک کردن اعتبار زیر 1200 تومن و توقف سرویس ارسال
        //        if (resultUsernfo[0].credit < 12000 && !PublicClass.Send1200)
        //        {
        //            string[] creditMessage = { $"اعتبار سامانه پیامکی {resultUsernfo[0].credit} ریال می باشد سرویس متوقف گردید" };
        //            var resultSend = sms.sendSms("iaubijar", "M4228056", from, vs, creditMessage, new string[] { }, "");
        //            if (resultSend[0] > 0)
        //            {
        //                Logger.WriteMessageSenderLog($"Send Credit 1000 to PhoneNumber :  {vs} , {resultSend[0]}");
        //                PublicClass.Send1200 = true;
        //            }
        //            return;
        //        }
        //        else
        //        {
        //            if (resultUsernfo[0].credit.Between(12001, 50000) && !PublicClass.Send5000)
        //            {
        //                string[] creditMessage = { $"اعتبار سامانه پیامکی {resultUsernfo[0].credit} ریال می باشد" };
        //                var resultCredit = sms.sendSms("iaubijar", "M4228056", from, vs, creditMessage, new string[] { }, "");
        //                if (resultCredit[0] > 0)
        //                {
        //                    Logger.WriteMessageSenderLog($"Send Credit 5000 to PhoneNumber :  {vs}  , {resultCredit[0]}");
        //                    PublicClass.Send5000 = true;
        //                }
        //            }
        //            if (resultUsernfo[0].credit > 50000)
        //            {
        //                PublicClass.Send1200 = PublicClass.Send5000 = false;
        //            }
        //            string[] to = new[] { mobile.ToString("00000000000") };
        //            string[] content = { $"{fullName} تولدت مبارک\r\n دبستان دخترانه سما بیجار" };
        //            var result = sms.sendSms("iaubijar", "M4228056", from, to, content, new string[] { }, "");

        //            if (result[0] > 0)
        //            {
        //                _birthRegisterRepository.InsertWithStudentId(id);
        //                _unitOfWork.SaveChanges();
        //                Logger.WriteMessageSenderLog($"Send Tavalood {mobile} , {result[0]}");
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Logger.WriteErrorLog(e, "SendTavalood");
        //    }
        //}
        //#endregion

    }
}