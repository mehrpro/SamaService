using System;

namespace DM.DTO
{
    public class TagRecorderDTO
    {
        public int ID { get; set; }
        public string TagID { get; set; }
        public DateTime DateTimeRegister { get; set; }
        public int MysqlID { get; set; }
        public bool SMS { get; set; }
        public bool Type { get; set; }
        public bool Enables { get; set; }
        public Nullable<int> TagID_FK { get; set; }
        public Nullable<int> StudentID_FK { get; set; } 
    }
}