namespace DM.Models
{
    public class Registered
    {
        public int ID { get; set; }

        public int AcademicYearID_FK { get; set; }
        public AcademicYear AcademicYear { get; set; }
        public int SchoolID_FK { get; set; }
        public School School { get; set; }
        public int ClassRoomID_FK { get; set; }
        public ClassRoom ClassRoom { get; set; }
        public int StudentID_FK { get; set; }
        public Student Student { get; set; }
        public bool Enabled { get; set; }





    }
}