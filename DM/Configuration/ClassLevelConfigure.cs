using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using DM.Models;

namespace DM.Configuration
{
    public class ClassLevelConfigure : EntityTypeConfiguration<ClassLevel>
    {
        public ClassLevelConfigure()
        {
            HasKey(x => x.ID);
            Property(x => x.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();
            Property(x => x.ClassLevelTitle).HasMaxLength(250);

            HasMany(x => x.ClassRooms)
                 .WithRequired(x => x.ClassLevel)
                 .HasForeignKey(x => x.ClassLevelID_FK)
                 .WillCascadeOnDelete(false);
        }
    }
}
