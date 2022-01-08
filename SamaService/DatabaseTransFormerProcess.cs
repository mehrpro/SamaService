using System;
using System.Linq;
using DM.Models;
using DM.Infrastructure;
using DM.Services;


namespace SamaService
{
    public class DatabaseTransFormerProcess
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITagRecorderRepository _tagRecorderRepository;
        //private readonly MySqlServiceRepository _mySqlServiceRepository;
        private readonly ILoggerRepository _loggerRepository;

        public DatabaseTransFormerProcess(IUnitOfWork unitOfWork,
                                          ILoggerRepository loggerRepository,
                                          ITagRecorderRepository tagRecorderRepository)
        {
            _unitOfWork = unitOfWork;
            _tagRecorderRepository = tagRecorderRepository;
            //_mySqlServiceRepository = MySqlServiceRepository;
            _loggerRepository = loggerRepository;
        }
        /// <summary>
        /// پردازش و انتقال بین دو سرور و ثبت و امحای تردد های ثبت شده
        /// </summary>
        public void TransformDataBase()
        {
            try
            {
                var listForDisableinMySql = MySqlServiceRepository.ReaderSQL();// لیست تگ های ثبت نشده
                foreach (var tagList in listForDisableinMySql)// ثبت در بانک اطلاعاتی اس کیوال
                {
                    _tagRecorderRepository.Insert(new TagRecorder()
                    {
                        TagID = tagList.Tag.Remove(tagList.Tag.Length - 1),
                        DateTimeRegister = tagList.dateRegister,
                        MysqlID = tagList.ID,
                        SMS = false,
                        Type = Convert.ToBoolean(tagList.TypeImport),
                        Enables = true,
                    });
                }
                var resultMysql = MySqlServiceRepository.UpdateTagRecordList(listForDisableinMySql.Select(x => x.ID).ToList());
                if (!resultMysql)
                {
                    _unitOfWork.SaveChanges();
                }
                else
                {
                    MySqlServiceRepository.RollbackTagRecordList(listForDisableinMySql.Select(x => x.ID).ToList());
                    _loggerRepository.WriteMessageLog("Error TransformDataBase");
                }

            }
            catch (Exception e)
            {
                _loggerRepository.WriteErrorLog(e, "TransformDataBase");

            }
        }

    }
}

