using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using DM.Models;

namespace DM.Configuration
{
    public class StudentTAGConfigure : EntityTypeConfiguration<StudentTAG>
    {
        public StudentTAGConfigure()
        {

            HasKey(x => x.ID);
            Property(x => x.ID).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Enabled).IsRequired();
            Property(x => x.StudentID_FK).IsRequired();
            Property(x => x.TagID_FK).IsRequired();

        }
    }
}