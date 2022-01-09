using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using SamaService.Infrastructure;
using SamaService.Models;

namespace SamaService.Services
{
    public interface IBirthRegisterRepository
    {
        void InsertWithStudentId(int studentId);
        List<BirthRegister> GetAllByCondition(Expression<Func<BirthRegister, bool>> expression);
    }

    public class BirthRegisterRepository : IBirthRegisterRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<BirthRegister> _birthRegisters;

        public BirthRegisterRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _birthRegisters = _unitOfWork.Set<BirthRegister>();
        }


        public void InsertWithStudentId(int studentId)
        {
            var newInsert = new BirthRegister();
            newInsert.StudentID_FK = studentId;
            newInsert.Registered = DateTime.Now;
            _birthRegisters.Add(newInsert);
        }

        public List<BirthRegister> GetAllByCondition(Expression<Func<BirthRegister, bool>> expression)
        {
            return _birthRegisters.Where(expression).ToList();
        }
    }
}