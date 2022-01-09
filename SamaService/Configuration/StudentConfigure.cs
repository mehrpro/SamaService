using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using SamaService.Models;

namespace SamaService.Configuration
{
    public class StudentConfigure : EntityTypeConfiguration<Student>
    {
        public StudentConfigure()
        {



            HasKey(x => x.ID);
            Property(x => x.ID).IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Enabled).IsRequired();
            Property(x => x.FName).HasMaxLength(50).IsRequired();
            Property(x => x.LName).HasMaxLength(100).IsRequired();
            Property(x => x.FatherName).HasMaxLength(50).IsRequired();
            Property(x => x.FatherPhone).HasMaxLength(11);
            Property(x => x.HomePhone).HasMaxLength(11);
            Property(x => x.FullName).HasMaxLength(151);
            Property(x => x.MotherName).HasMaxLength(50);
            Property(x => x.MotherPhone).HasMaxLength(11);
            Property(x => x.StudentCode).HasMaxLength(20);
            Property(x => x.StudentNatinalCode).HasMaxLength(11);
            Property(x => x.SMS).HasMaxLength(11).IsRequired();
            Property(x => x.BrithDate).HasColumnType("date").IsRequired();
            Property(x => x.RegDate).HasColumnType("datetime").IsRequired();
            HasMany(x => x.Registereds)
                .WithRequired(x => x.Student)
                .HasForeignKey(x => x.StudentID_FK)
                .WillCascadeOnDelete(false);
            HasMany(x => x.StudentTAGs)
                .WithRequired(x => x.Student)
                .HasForeignKey(x => x.StudentID_FK)
                .WillCascadeOnDelete(false);

            HasMany(x => x.BirthRegisters)
            .WithRequired(x => x.Student)
            .HasForeignKey(x => x.StudentID_FK)
            .WillCascadeOnDelete(false);
        }
    }
}