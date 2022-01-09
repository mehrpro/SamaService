using AutoMapper;
using SamaService.DTO;
using SamaService.Models;

namespace SamaService.Infrastructure
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Tag, TagDTO>().ReverseMap();
        }
    }
}