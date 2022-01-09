using System.ComponentModel.DataAnnotations.Schema;
using SamaService.Models;
using System.Data.Entity.ModelConfiguration;
namespace SamaService.Configuration
{


    public class AcademicYearConfigure : EntityTypeConfiguration<AcademicYear>
    {
        public AcademicYearConfigure()
        {
            HasKey(x => x.ID);
            Property(x => x.ID).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Enabled).IsRequired();
            Property(x => x.Description).HasMaxLength(250);
            Property(x => x.YearsFinish).IsRequired().HasColumnType("datetime");
            Property(x => x.YearsStart).IsRequired().HasColumnType("datetime");
            Property(x => x.YearsName).IsRequired().HasMaxLength(250);
            HasMany(x => x.Registereds)
                .WithRequired(x => x.AcademicYear)
                .HasForeignKey(x => x.AcademicYearID_FK)
                .WillCascadeOnDelete(false);
        }
    }
}