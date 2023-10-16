using AutoMapper;
using Dor.Blog.Application.Authorization;

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

            //CreateMap<CredentialDTO, Credential>();
            ////Specific            
            ///
            CreateMap<CredentialDTO, Credential>();
                //.ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                //.ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));
        }
    }
}
