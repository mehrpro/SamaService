using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using SamaService.Infrastructure;
using SamaService.Models;

namespace SamaService.Services
{
    public interface IStudentTagRepository
    {
        StudentTAG GetFirstOrDefault(Expression<Func<StudentTAG, bool>> expression);
    }

    public class StudentTagRepository : IStudentTagRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<StudentTAG> _studentTags;


        public StudentTagRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _studentTags = _unitOfWork.Set<StudentTAG>();
        }

        public StudentTAG GetFirstOrDefault(Expression<Func<StudentTAG, bool>> expression)
        {
            return _studentTags.FirstOrDefault(expression);
        }
    }
}