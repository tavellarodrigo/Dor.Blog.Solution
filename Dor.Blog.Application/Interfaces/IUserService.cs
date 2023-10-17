using Dor.Blog.Domain.Entities;

namespace Dor.Blog.Application.Interfaces
{
    public interface IUserService : IService<User>
    {
        Task<User> CreateAsync(User user,string password);
    }
}
