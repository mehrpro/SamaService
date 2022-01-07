using System;

namespace DM.Models
{

    public class BirthRegister
    {
        public int ID { get; set; }
        public int StudentID_FK { get; set; }
        public Student Student { get; set; }
        public DateTime Registered { get; set; }
    }

}