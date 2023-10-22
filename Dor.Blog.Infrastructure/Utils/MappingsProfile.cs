using AutoMapper;
using Dor.Blog.Application.Authorization;
using Dor.Blog.Application.DTO;
using Dor.Blog.Domain.Entities;

namespace Dor.Blog.Infrastructure.Utils
{
    /// <summary>
    /// configure Auto Mapper
    /// </summary>
    public class MappingsProfile : Profile
    {
        public MappingsProfile()
        {
            CreateMap<CredentialDTO, Credential>();
         
            CreateMap<UserDTO, User>();
        
            CreateMap<PostForCreateDTO, BlogPost>();

            CreateMap<PostDTO, BlogPost>();            

            CreateMap<BlogPost, BlogPost>()
                .ForSourceMember(source => source.Updated, opt => opt.DoNotValidate())
                .ForSourceMember(source => source.Deleted, opt => opt.DoNotValidate())
                .ForSourceMember(source => source.Id, opt => opt.DoNotValidate());
            
            CreateMap<PostForUpdateDTO, BlogPost>();
        }
    }
}
