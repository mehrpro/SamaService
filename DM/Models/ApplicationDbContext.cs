using System.Data.Entity;
using DM.Configuration;
using DM.Infrastructure;


namespace DM.Models
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public ApplicationDbContext() : base("name=schooldbEntities2")
        {

        }


        #region IUnitOfWork Members
        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public new void SaveChanges()
        {
            base.SaveChanges();
        }

        #endregion



        protected override void OnModelCreating(DbModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Configurations.Add(new AcademicYearConfigure());
            builder.Configurations.Add(new BirthRegisterConfigure());
            builder.Configurations.Add(new ClassLevelConfigure());
            builder.Configurations.Add(new ClassRoomConfigure());
            builder.Configurations.Add(new RegisteredConfigure());
            builder.Configurations.Add(new SchoolConfigure());
            builder.Configurations.Add(new StudentConfigure());
            builder.Configurations.Add(new StudentTAGConfigure());
            builder.Configurations.Add(new TagConfigure());
            builder.Configurations.Add(new TagRecorderCongifure());
            builder.Configurations.Add(new UserConfigure());
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