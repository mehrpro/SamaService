using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using DM.Models;

namespace DM.Configuration
{
    public class TagConfigure : EntityTypeConfiguration<Tag>
    {
        public TagConfigure()
        {





            HasKey(x => x.ID);
            Property(x => x.ID).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.TagID_HEX).HasMaxLength(50).IsRequired();
            Property(x => x.CartView).HasMaxLength(10);
            Property(x => x.Enabled).IsRequired();
            Property(x => x.DeleteTAG).IsRequired();
            HasMany(x => x.StudentTAGs)
                .WithRequired(x => x.Tag)
                .HasForeignKey(x => x.TagID_FK)
                .WillCascadeOnDelete(false);
        }
    }
}