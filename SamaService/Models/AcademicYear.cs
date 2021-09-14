
namespace SamaService.Models
{
    using System;
    using System.Collections.Generic;

    public class AcademicYear
    {
        public AcademicYear()
        {
            Registereds = new HashSet<Registered>();
        }

        public int ID { get; set; }
        public string YearsName { get; set; }
        public DateTime YearsStart { get; set; }
        public DateTime YearsFinish { get; set; }
        public bool Enabled { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Registered> Registereds { get; set; }
    }
}