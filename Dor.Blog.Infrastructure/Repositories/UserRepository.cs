using Dor.Blog.Application.Interfaces;
using Dor.Blog.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;
using System.Transactions;

namespace Dor.Blog.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;        

        public UserRepository(UserManager<User> userManager)
        {
            _userManager = userManager;            
        }

        public Task AddAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public Task AddRangeAsync(IEnumerable<User> entities)
        {
            throw new NotImplementedException();
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
    }
}

