using System;

namespace SamaService.DTO
{
    public class TagListDTO
    {
        public int ID { get; set; }
        public string Tag { get; set; }
        public DateTime dateRegister { get; set; }
        public int Reg { get; set; }
        public int TypeImport { get; set; }
    }
}