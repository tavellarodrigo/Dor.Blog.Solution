using Dor.Blog.Domain.Entities;
using Dor.Blog.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using System.Transactions;

namespace Dor.Blog.Infrastructure
{

    /// <summary>
    /// To seed the DB
    /// Create a User with admin Role
    /// </summary>
    public class DbInitializer
    {          
        public async Task InitializeAsync(UserManager<User> _userManager, RoleManager<IdentityRole> _roleManager, DataContext _dbContext)
        {   
            ArgumentNullException.ThrowIfNull(_dbContext, nameof(_dbContext));
            _dbContext.Database.EnsureCreated();

            if (_dbContext.Users.Any()) 
                return;

            //admin user that must be authenticated to create other users
            User user = new User
            {
                Name = "Admin Name",
                LastName = "Admin LastName",
                Status = 2,
                UserName = "administrator",
                Email = "admin@dor.com",
                PhoneNumber = "+54351123456789"
            };

            user.RoleNames = new string[] { "Admin" };

            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _roleManager.CreateAsync(new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" });
                var result = await _userManager.CreateAsync(user, "Hello.123#$");
                if (result.Succeeded)
                {
                    await _userManager.AddToRolesAsync(user, user.RoleNames);
                }
                scope.Complete();
            }

            //user that can be used to create posts
            user = new User
            {
                Name = "Lionel",
                LastName = "Messi",
                Status = 2,
                UserName = "lionel.messi",
                Email = "lionel@dor.com",
                PhoneNumber = "+54351123456999"
            };

            user.RoleNames = new string[] { "User" };

            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _roleManager.CreateAsync(new IdentityRole { Id = "2", Name = "User", NormalizedName = "USER" });
                var result = await _userManager.CreateAsync(user, "Hello.123#$");
                if (result.Succeeded)
                {
                    await _userManager.AddToRolesAsync(user, user.RoleNames);
                }
                scope.Complete();
            }
        }
    }   
}
