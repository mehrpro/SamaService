using System.Data.Entity.ModelConfiguration;
using DM.Models;

namespace DM.Configuration
{
    public class SchoolConfigure : EntityTypeConfiguration<School>
    {
        public SchoolConfigure()
        {
            HasKey(x => x.ID);
            Property(x => x.SchoolAddress).HasMaxLength(250);
            Property(x => x.SchoolName).HasMaxLength(250).IsRequired();
            Property(x => x.SchoolTel).HasMaxLength(15);
            HasMany(x => x.Registereds)
                .WithRequired(x => x.School)
                .HasForeignKey(x => x.SchoolID_FK)
                .WillCascadeOnDelete(false);
        }
    }
}