using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using SamaService.Models;

namespace SamaService.Configuration
{
    public class ClassRoomConfigure : EntityTypeConfiguration<ClassRoom>
    {
        public ClassRoomConfigure()
        {
            HasKey(x => x.ID);
            Property(x => x.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.ClassName).HasMaxLength(250).IsRequired();
            Property(x => x.ClassRoomEnable).IsRequired();
            Property(x => x.ClassRegisterDate).IsRequired().HasColumnType("datetime");
            HasMany(x => x.Registereds)
                .WithRequired(x => x.ClassRoom)
                .HasForeignKey(x => x.ClassRoomID_FK)
                .WillCascadeOnDelete(false);
        }
    }
}