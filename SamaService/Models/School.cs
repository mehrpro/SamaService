namespace SamaService.Models
{
    using System.Collections.Generic;

    public partial class School
    {
        public School()
        {
            Registereds = new HashSet<Registered>();
        }

        public int ID { get; set; }
        public string SchoolName { get; set; }
        public string SchoolAddress { get; set; }
        public string SchoolTel { get; set; }

        public virtual ICollection<Registered> Registereds { get; set; }
    }
}