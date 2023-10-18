using AutoMapper;
using Dor.Blog.Application.Authorization;
using Dor.Blog.Application.DTO;
using Dor.Blog.Domain.Entities;

namespace Dor.Blog.Infrastructure.Utils
{
    public class MappingsProfile : Profile
    {
        public MappingsProfile()
        {
            ////Generals
            //CreateMap<string, DateTime>().ConvertUsing(x => DateTime.ParseExact(x, Constants.DateTimeStringFormat, CultureInfo.InvariantCulture));
            //CreateMap<DateTime, string>().ConvertUsing(x => x.ToString(Constants.DateTimeStringFormat));
            //CreateMap<string, DateTime?>().ConvertUsing(x => x != null ? DateTime.ParseExact(x, Constants.DateTimeStringFormat, CultureInfo.InvariantCulture) : null);           
            //ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.Now));
            

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
