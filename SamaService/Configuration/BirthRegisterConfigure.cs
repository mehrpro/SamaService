using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using SamaService.Models;

namespace SamaService.Configuration
{
    public class BirthRegisterConfigure : EntityTypeConfiguration<BirthRegister>
    {
        public BirthRegisterConfigure()
        {
            HasKey(x => x.ID);
            Property(x => x.ID).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.StudentID_FK).IsRequired();
            Property(x => x.Registered).IsRequired().HasColumnType("date");
        }


    }
}