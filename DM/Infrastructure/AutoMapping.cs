using AutoMapper;
using DM.DTO;
using DM.Models;

namespace DM.Infrastructure
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Tag, TagDTO>().ReverseMap();
        }
    }
}