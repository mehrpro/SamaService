using System.Data.Entity;
using SamaService.Infrastructure;
using SamaService.Models;

namespace SamaService.Services
{
    public interface ITagRepository : IRepositoryBase<Tag>
    {

    }

    public class TagRepository : RepositoryBase<Tag>, ITagRepository
    {
        public TagRepository(DbContext context) : base(context)
        {

        }
    }
}