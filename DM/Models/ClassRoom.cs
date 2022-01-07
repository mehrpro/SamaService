using System;

namespace DM.Models
{

    using System.Collections.Generic;

    public class ClassRoom
    {
        public ClassRoom()
        {
            Registereds = new HashSet<Registered>();
        }

        public int ID { get; set; }
        public string ClassName { get; set; }
        public int ClassLevelID_FK { get; set; }
        public ClassLevel ClassLevel { get; set; }
        public DateTime ClassRegisterDate { get; set; }
        public bool ClassRoomEnable { get; set; }


        public virtual ICollection<Registered> Registereds { get; set; }
    }
}