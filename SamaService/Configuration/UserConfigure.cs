using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using SamaService.Models;

namespace SamaService.Configuration
{
    public class UserConfigure : EntityTypeConfiguration<User>
    {
        public UserConfigure()
        {


            HasKey(x => x.ID);
            Property(x => x.ID).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.FName).HasMaxLength(50).IsRequired();
            Property(x => x.LName).HasMaxLength(50).IsRequired();
            Property(x => x.UserName).HasMaxLength(50).IsRequired();
            Property(x => x.Mobile).HasMaxLength(12).IsRequired();
            Property(x => x.Password).HasMaxLength(120).IsRequired();
        }
    }
}