using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DM.Infrastructure;
using DM.Models;

namespace DM.Services
{
    public interface IStudentRepository
    {
        void Insert(Student student);
        List<Student> GetAll();
        Student GetFirstOrDefault(Expression<Func<Student, bool>> expression);
        Student GetById(int studentId);
    }

    public class StudentRepository : IStudentRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private IDbSet<Student> _students;

        public StudentRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _students = _unitOfWork.Set<Student>();
        }

        public void Insert(Student student)
        {
            _students.Add(student);

        }

        public List<Student> GetAll()
        {
            return _students.ToList();
        }

        public Student GetFirstOrDefault(Expression<Func<Student, bool>> expression)
        {
            return _students.FirstOrDefault(expression);
        }

        public Student GetById(int studentId)
        {
            return _students.Find(studentId);
        }
    }
}