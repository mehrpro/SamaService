using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SamaService.Infrastructure;
using SamaService.Services;
using SamaService.DTO;

namespace SamaService.Process
{
    public class NewTags
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerRepository _logger;
        private readonly ITagRecorderRepository _tagRecorderRepository;
        private readonly ITagRepository _tagRepository;

        public NewTags(IUnitOfWork unitOfWork,
                       ILoggerRepository logger,
                       ITagRecorderRepository tagRecorderRepository,
                       ITagRepository tagRepository)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _tagRecorderRepository = tagRecorderRepository;
            _tagRepository = tagRepository;
        }
        public void Run()
        {
            try
            {
                _logger.WriteMessageLog("NewTag");
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
                            CartView = PublicStaticClass.HexToDecimal(item)
                        };
                        _tagRepository.Insert(newTag);
                        _unitOfWork.SaveChanges();
                        _logger.WriteMessageLog($"Save New TAG : {item} ViewLabel {PublicStaticClass.HexToDecimal(item)}");
                    }
                }
            }
            catch (Exception e)
            {
                _logger.WriteErrorLog(e, "NewTags");
            }

        }
    }
}
