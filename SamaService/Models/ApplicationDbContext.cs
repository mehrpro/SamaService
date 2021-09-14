using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;


namespace SamaService.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("name=schooldbEntities")
        {

        }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AcademicYear>().HasKey(x => x.ID);
            builder.Entity<AcademicYear>().Property(x => x.ID).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            builder.Entity<AcademicYear>().Property(x => x.Enabled).IsRequired();
            builder.Entity<AcademicYear>().Property(x => x.Description).HasMaxLength(250);
            builder.Entity<AcademicYear>().Property(x => x.YearsFinish).IsRequired().HasColumnType("datetime");
            builder.Entity<AcademicYear>().Property(x => x.YearsStart).IsRequired().HasColumnType("datetime");
            builder.Entity<AcademicYear>().Property(x => x.YearsName).IsRequired().HasMaxLength(250);
            builder.Entity<AcademicYear>()
                .HasMany(x => x.Registereds)
                .WithRequired(x => x.AcademicYear)
                .HasForeignKey(x => x.AcademicYearID_FK)
                .WillCascadeOnDelete(false);


            builder.Entity<BirthRegister>().HasKey(x => x.ID);
            builder.Entity<BirthRegister>().Property(x => x.ID).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            builder.Entity<BirthRegister>().Property(x => x.StudentID_FK).IsRequired();
            builder.Entity<BirthRegister>().Property(x => x.Registered).IsRequired().HasColumnType("date");



            builder.Entity<ClassLevel>().HasKey(x => x.ID);
            builder.Entity<ClassLevel>().Property(x => x.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();
            builder.Entity<ClassLevel>().Property(x => x.ClassLevelTitle).HasMaxLength(250);
            builder.Entity<ClassLevel>()
                .HasMany(x => x.ClassRooms)
                .WithRequired(x => x.ClassLevel)
                .HasForeignKey(x => x.ClassLevelID_FK)
                .WillCascadeOnDelete(false);

            builder.Entity<ClassRoom>().HasKey(x => x.ID);
            builder.Entity<ClassRoom>().Property(x => x.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            builder.Entity<ClassRoom>().Property(x => x.ClassName).HasMaxLength(250).IsRequired();
            builder.Entity<ClassRoom>().Property(x => x.ClassRoomEnable).IsRequired();
            builder.Entity<ClassRoom>().Property(x => x.ClassRegisterDate).IsRequired().HasColumnType("datetime");
            builder.Entity<ClassRoom>()
                .HasMany(x => x.Registereds)
                .WithRequired(x => x.ClassRoom)
                .HasForeignKey(x => x.ClassRoomID_FK)
                .WillCascadeOnDelete(false);

            builder.Entity<Registered>().HasKey(x => x.ID);
            builder.Entity<Registered>().Property(x => x.ID).IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            builder.Entity<Registered>().Property(x => x.Enabled).IsRequired();

            builder.Entity<School>().HasKey(x => x.ID);
            builder.Entity<School>().Property(x => x.SchoolAddress).HasMaxLength(250);
            builder.Entity<School>().Property(x => x.SchoolName).HasMaxLength(250).IsRequired();
            builder.Entity<School>().Property(x => x.SchoolTel).HasMaxLength(15);
            builder.Entity<School>()
                .HasMany(x => x.Registereds)
                .WithRequired(x => x.School)
                .HasForeignKey(x => x.SchoolID_FK)
                .WillCascadeOnDelete(false);

            builder.Entity<Student>().HasKey(x => x.ID);
            builder.Entity<Student>().Property(x => x.ID).IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            builder.Entity<Student>().Property(x => x.Enabled).IsRequired();
            builder.Entity<Student>().Property(x => x.FName).HasMaxLength(50).IsRequired();
            builder.Entity<Student>().Property(x => x.LName).HasMaxLength(100).IsRequired();
            builder.Entity<Student>().Property(x => x.FatherName).HasMaxLength(50).IsRequired();
            builder.Entity<Student>().Property(x => x.FatherPhone).HasMaxLength(11);
            builder.Entity<Student>().Property(x => x.HomePhone).HasMaxLength(11);
            builder.Entity<Student>().Property(x => x.FullName).HasMaxLength(151);
            builder.Entity<Student>().Property(x => x.MotherName).HasMaxLength(50);
            builder.Entity<Student>().Property(x => x.MotherPhone).HasMaxLength(11);
            builder.Entity<Student>().Property(x => x.StudentCode).HasMaxLength(20);
            builder.Entity<Student>().Property(x => x.StudentNatinalCode).HasMaxLength(11);
            builder.Entity<Student>().Property(x => x.SMS).HasMaxLength(11).IsRequired();
            builder.Entity<Student>().Property(x => x.BrithDate).HasColumnType("date").IsRequired();
            builder.Entity<Student>().Property(x => x.RegDate).HasColumnType("datetime").IsRequired();
            builder.Entity<Student>()
                .HasMany(x => x.Registereds)
                .WithRequired(x => x.Student)
                .HasForeignKey(x => x.StudentID_FK)
                .WillCascadeOnDelete(false);
            builder.Entity<Student>()
                .HasMany(x => x.StudentTAGs)
                .WithRequired(x => x.Student)
                .HasForeignKey(x => x.StudentID_FK)
                .WillCascadeOnDelete(false);


            builder.Entity<StudentTAG>().HasKey(x => x.ID);
            builder.Entity<StudentTAG>().Property(x => x.ID).IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            builder.Entity<StudentTAG>().Property(x => x.Enabled).IsRequired();
            builder.Entity<StudentTAG>().Property(x => x.StudentID_FK).IsRequired();
            builder.Entity<StudentTAG>().Property(x => x.TagID_FK).IsRequired();


            builder.Entity<TagRecorder>().HasKey(x => x.ID);
            builder.Entity<TagRecorder>().Property(x => x.ID).IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            builder.Entity<TagRecorder>().Property(x => x.Enables).IsRequired();
            builder.Entity<TagRecorder>().Property(x => x.TagID).IsRequired().HasMaxLength(50);
            builder.Entity<TagRecorder>().Property(x => x.DateTimeRegister).IsRequired().HasColumnType("datetime");
            builder.Entity<TagRecorder>().Property(x => x.MysqlID).IsRequired();
            builder.Entity<TagRecorder>().Property(x => x.SMS).IsRequired();
            builder.Entity<TagRecorder>().Property(x => x.Type).IsRequired();

            builder.Entity<Tag>().HasKey(x => x.ID);
            builder.Entity<Tag>().Property(x => x.ID).IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            builder.Entity<Tag>().Property(x => x.TagID_HEX).HasMaxLength(50).IsRequired();
            builder.Entity<Tag>().Property(x => x.CartView).HasMaxLength(10);
            builder.Entity<Tag>().Property(x => x.Enabled).IsRequired();
            builder.Entity<Tag>().Property(x => x.DeleteTAG).IsRequired();
            builder.Entity<Tag>()
                .HasMany(x => x.StudentTAGs)
                .WithRequired(x => x.Tag)
                .HasForeignKey(x => x.TagID_FK)
                .WillCascadeOnDelete(false);



            builder.Entity<User>().HasKey(x => x.ID);
            builder.Entity<User>().Property(x => x.ID).IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            builder.Entity<User>().Property(x => x.FName).HasMaxLength(50).IsRequired();
            builder.Entity<User>().Property(x => x.LName).HasMaxLength(50).IsRequired();
            builder.Entity<User>().Property(x => x.UserName).HasMaxLength(50).IsRequired();
            builder.Entity<User>().Property(x => x.Mobile).HasMaxLength(12).IsRequired();
            builder.Entity<User>().Property(x => x.Password).HasMaxLength(120).IsRequired();





        }

        public virtual DbSet<AcademicYear> AcademicYears { get; set; }
        public virtual DbSet<ClassLevel> ClassLevels { get; set; }
        public virtual DbSet<ClassRoom> ClassRooms { get; set; }
        public virtual DbSet<Registered> Registereds { get; set; }
        public virtual DbSet<School> Schools { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<StudentTAG> StudentTAGs { get; set; }
        public virtual DbSet<TagRecorder> TagRecorders { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<BirthRegister> BirthRegisters { get; set; }
    }
}