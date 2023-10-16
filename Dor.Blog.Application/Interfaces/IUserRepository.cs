using Dor.Blog.Domain.Entities;

namespace Dor.Blog.Application.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> CreateUser(User User, String Password);
        Task<User> GetUserById(int id);
        
        Task<IEnumerable<User>> GetUsers();        
        Task<User> GetUserRoles(User user);

        Task<User> AddUserRoles(User user, IEnumerable<string> rolesForAdd, IEnumerable<string> rolesForExclude);

        Task<User> RemoveUserRoles(User user, IEnumerable<string> rolesForRemove, IEnumerable<string> rolesForExclude);

        Task<User> UpdateUser(User userForBeUpdated);
        Task<User?> GetUserByUserName(string userName);
        Task<User> ResetPassword(User user, string newPassword);
    }
}