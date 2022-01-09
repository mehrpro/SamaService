using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using SamaService.DTO;
using SamaService.Infrastructure;
using SamaService.Models;

namespace SamaService.Services
{
    public interface ITagRepository
    {
        void Insert(TagDTO tag);
        Tag GetFirstOrDefualt(Expression<Func<Tag, bool>> expression);
    }

    public class TagRepository : ITagRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<Tag> _tags;
        private readonly IMapper _mapper;

        public TagRepository(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _tags = _unitOfWork.Set<Tag>();
        }

        public void Insert(TagDTO tag)
        {
            var resultMap = _mapper.Map<Tag>(tag);
            _tags.Add(resultMap);
        }

        public Tag GetFirstOrDefualt(Expression<Func<Tag, bool>> expression)
        {
            return _tags.FirstOrDefault(expression);
        }
    }
}