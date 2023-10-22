using Dor.Blog.Application.Interfaces;
using Dor.Blog.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Transactions;

namespace Dor.Blog.Infrastructure.Repositories
{
    /// <summary>
    /// Use Identity UserManager
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;        

        public UserRepository(UserManager<User> userManager)
        {
            _userManager = userManager;            
        }  

        public async Task<User> CreateUser(User User, String Password)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var result = await _userManager.CreateAsync(User, Password);
                if (result.Succeeded)
                {                    
                    await _userManager.AddToRolesAsync(User, User.RoleNames);
                    scope.Complete();
                }
                else
                {
                    scope.Dispose();
                    throw new Exception(result.Errors.First().Description);
                }
                
            }
            return User;
        }

        public async Task<IEnumerable<User>?> GetAll()
        {            
            var users = await _userManager.Users.ToListAsync();
            foreach (var user in users)
            {
                user.RoleNames = await _userManager.GetRolesAsync(user);
            }
            
            return users;
        }

        public async Task<User?> GetUserByUserName(string userName)
        { 

            User? userSearched = await _userManager.FindByNameAsync(userName);
            
            if (userSearched == null)
                return null;

            //get role list
            var roles = await _userManager.GetRolesAsync(userSearched);
            userSearched.RoleNames = roles;
            
            return userSearched;
        }

        public async Task<User?> SingleOrDefaultAsync(Expression<Func<User, bool>> predicate)
        {
            return await _userManager.Users.FirstOrDefaultAsync(predicate);
        }
    }
}

