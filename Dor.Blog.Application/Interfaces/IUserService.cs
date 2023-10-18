using Dor.Blog.Domain.Entities;

namespace Dor.Blog.Application.Interfaces
{
    public interface IUserService 
    {
        Task <BaseResponse<User>> CreateAsync(User user,string password);
    }
}
