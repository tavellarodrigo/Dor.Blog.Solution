using Dor.Blog.Domain.Entities;
using System.Linq.Expressions;

namespace Dor.Blog.Application.Interfaces
{
    public interface IUserService 
    {
        Task <BaseResponse<User>> CreateAsync(User user,string password);
        Task <BaseResponse<IEnumerable<User>>> GetAllAsync();
        Task<User?> SingleOrDefaultAsync(Expression<Func<User, bool>> predicate);
    }
}
