using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using DM.Models;

namespace DM.Configuration
{
    public class TagRecorderCongifure : EntityTypeConfiguration<TagRecorder>
    {
        public TagRecorderCongifure()
        {
            HasKey(x => x.ID);
            Property(x => x.ID).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Enables).IsRequired();
            Property(x => x.TagID).IsRequired().HasMaxLength(50);
            Property(x => x.DateTimeRegister).IsRequired().HasColumnType("datetime");
            Property(x => x.MysqlID).IsRequired();
            Property(x => x.SMS).IsRequired();
            Property(x => x.Type).IsRequired();
        }
    }
}