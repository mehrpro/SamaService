using AutoMapper;
using SamaService.Models;
using SamaService.Services;
using StructureMap;
namespace SamaService.Infrastructure
{
    public class TypeRegistery : Registry
    {
        public TypeRegistery()
        {
            For<IUnitOfWork>().Use<ApplicationDbContext>();
            For<IUnitOfWorkClass>().Use<UnitOfWorkClass>();
            For<IStudentRepository>().Use<StudentRepository>();
            For<IStudentTagRepository>().Use<StudentTagRepository>();
            For<ITagRepository>().Use<TagRepository>();
            For<ITagRecorderRepository>().Use<TagRecorderRepository>();
            For<IBirthRegisterRepository>().Use<BirthRegisterRepository>();
            //For<IMySqlServiceRepository>().Use<MySqlServiceRepository>();
            For<ILoggerRepository>().Use<LoggerRepository>();
            For<ISenderRepository>().Use<SenderRepository>();

            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(typeof(AutoMapping));
            });
            var mapper = mapperConfiguration.CreateMapper();
            For<IMapper>().Use(mapper);
        }
    }
}