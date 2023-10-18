using Dor.Blog.Domain.Entities;

namespace Dor.Blog.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User> CreateUser(User User, String Password);     
        Task<User?> GetUserByUserName(string userName);       
    }
}