using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using SamaService.Models;

namespace SamaService.Configuration
{
    public class RegisteredConfigure : EntityTypeConfiguration<Registered>
    {
        public RegisteredConfigure()
        {

            HasKey(x => x.ID);
            Property(x => x.ID).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Enabled).IsRequired();
        }
    }
}