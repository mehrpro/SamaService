namespace DM.Models
{
    using System.Collections.Generic;

    public class ClassLevel
    {

        public ClassLevel()
        {
            ClassRooms = new HashSet<ClassRoom>();
        }

        public int ID { get; set; }
        public string ClassLevelTitle { get; set; }

        public virtual ICollection<ClassRoom> ClassRooms { get; set; }
    }
}