namespace SamaService.Models
{
    using System.Collections.Generic;

    public class Tag
    {
        public Tag()
        {
            StudentTAGs = new HashSet<StudentTAG>();
        }

        public int ID { get; set; }
        public string TagID_HEX { get; set; }
        public bool Enabled { get; set; }
        public bool DeleteTAG { get; set; }
        public string CartView { get; set; }

        public virtual ICollection<StudentTAG> StudentTAGs { get; set; }
    }
}