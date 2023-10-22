using AutoMapper;
using Dor.Blog.Application.Interfaces;

namespace Dor.Blog.Infrastructure.Utils
{
    /// <summary>
    /// for entity mapping Auto Mapper
    /// </summary>
    public class MapperUtil : IMapperUtil
    {
        private readonly IMapper _mapper;

        public MapperUtil(IMapper mapper)
        {
            _mapper = mapper;
        }

        public TDestination Map<TOrigin, TDestination>(TOrigin value)
        {
            return _mapper.Map<TDestination>(value);
        }
    }
}
