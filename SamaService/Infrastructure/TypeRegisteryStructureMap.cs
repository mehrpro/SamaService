using SamaService.Models;
using StructureMap;

namespace SamaService.Infrastructure
{
    public class TypeRegisteryStructureMap : Registry
    {
        public TypeRegisteryStructureMap()
        {
            For<UnitOfWork<ApplicationDbContext>>();
        }
    }
}