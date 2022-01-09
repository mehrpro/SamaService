namespace SamaService.Models
{


    public class StudentTAG
    {
        public int ID { get; set; }
        public int TagID_FK { get; set; }
        public Tag Tag { get; set; }
        public int StudentID_FK { get; set; }
        public Student Student { get; set; }
        public bool Enabled { get; set; }
    }
}