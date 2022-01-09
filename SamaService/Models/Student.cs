

namespace SamaService.Models
{

    using System;
    using System.Collections.Generic;

    public class Student
    {
        public Student()
        {
            Registereds = new HashSet<Registered>();
            StudentTAGs = new HashSet<StudentTAG>();
            BirthRegisters = new HashSet<BirthRegister>();
        }

        public int ID { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string FullName { get; set; }
        public string StudentCode { get; set; }
        public string StudentNatinalCode { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string HomePhone { get; set; }
        public string FatherPhone { get; set; }
        public string MotherPhone { get; set; }
        public string SMS { get; set; }
        public DateTime BrithDate { get; set; }

        public DateTime RegDate { get; set; }
        public bool Enabled { get; set; }
        public byte[] Picture { get; set; }

        public virtual ICollection<Registered> Registereds { get; set; }
        public virtual ICollection<StudentTAG> StudentTAGs { get; set; }
        public virtual ICollection<BirthRegister> BirthRegisters { get; set; }

    }
}