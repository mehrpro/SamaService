using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DM.Infrastructure;
using DM.Models;

namespace DM.Services
{
    public interface ITagRecorderRepository
    {
        IList<TagRecorder> GetAll();
        List<TagRecorder> GetAllByCondition(Expression<Func<TagRecorder, bool>> expression);
        void DeleteRange(List<TagRecorder> list);
        bool GetAny(Expression<Func<TagRecorder, bool>> expression);
        TagRecorder GetById(int Id);
        void Insert(TagRecorder tagRecorder);
    }

    public class TagRecorderRepository : ITagRecorderRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<TagRecorder> _tagRecorder;
        public TagRecorderRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _tagRecorder = _unitOfWork.Set<TagRecorder>();
        }

        public IList<TagRecorder> GetAll()
        {
            return _tagRecorder.ToList();
        }

        public List<TagRecorder> GetAllByCondition(Expression<Func<TagRecorder, bool>> expression)
        {
            return _tagRecorder.Where(expression).ToList();
        }

        public void DeleteRange(List<TagRecorder> list)
        {
            foreach (var item in list)
            {
                _tagRecorder.Remove(item);
            }
        }

        public bool GetAny(Expression<Func<TagRecorder, bool>> expression)
        {
            return _tagRecorder.Any(expression);
        }

        public TagRecorder GetById(int Id)
        {
            return _tagRecorder.Find(Id);
        }

        public void Insert(TagRecorder tagRecorder)
        {
            _tagRecorder.Add(tagRecorder);
        }
    }


}