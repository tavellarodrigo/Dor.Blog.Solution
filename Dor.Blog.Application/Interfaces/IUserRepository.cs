using Dor.Blog.Domain.Entities;
using System.Linq.Expressions;

namespace Dor.Blog.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User> CreateUser(User User, String Password);     
        Task<User?> GetUserByUserName(string userName);
        Task<IEnumerable<User>?> GetAll();
        Task<User?> SingleOrDefaultAsync(Expression<Func<User, bool>> predicate);
    }
}