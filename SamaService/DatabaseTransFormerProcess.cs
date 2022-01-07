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
        private readonly IMySqlServiceRepository _mySqlServiceRepository;

        public DatabaseTransFormerProcess(IUnitOfWork unitOfWork, ITagRecorderRepository tagRecorderRepository, IMySqlServiceRepository mySqlServiceRepository)
        {
            _unitOfWork = unitOfWork;
            _tagRecorderRepository = tagRecorderRepository;
            _mySqlServiceRepository = mySqlServiceRepository;
        }
        /// <summary>
        /// پردازش و انتقال بین دو سرور و ثبت و امحای تردد های ثبت شده
        /// </summary>
        public void TransformDataBase()
        {
            try
            {
                var listForDisableinMySql = _mySqlServiceRepository.ReaderSQL();// لیست تگ های ثبت نشده
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
                var resultMysql = _mySqlServiceRepository.UpdateTagRecordList(listForDisableinMySql.Select(x => x.ID).ToList());
                if (!resultMysql)
                {
                    _unitOfWork.SaveChanges();
                }
                else
                {
                    _mySqlServiceRepository.rollbackTagRecordList(listForDisableinMySql.Select(x => x.ID).ToList());
                    Logger.WriteMessageLog("Error TransformDataBase");
                }

            }
            catch (Exception e)
            {
                Logger.WriteErrorLog(e, "TransformDataBase");

            }
        }

    }
}

