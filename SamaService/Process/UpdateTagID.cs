using SamaService.Infrastructure;
using SamaService.Services;
using SamaService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamaService.Process
{
    public class UpdateTagID
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStudentTagRepository _studentTagRepository;
        private readonly ITagRecorderRepository _tagRecorderRepository;
        private readonly ILoggerRepository _loggerRepository;
        public UpdateTagID(IUnitOfWork unitOfWork,
                           IStudentTagRepository studentTagRepository,
                           ITagRecorderRepository tagRecorderRepository,
                           ILoggerRepository loggerRepository)
        {
            _unitOfWork = unitOfWork;
            _studentTagRepository = studentTagRepository;
            _tagRecorderRepository = tagRecorderRepository;
            _loggerRepository = loggerRepository;
        }

        public void Run()
        {
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
    }
}

