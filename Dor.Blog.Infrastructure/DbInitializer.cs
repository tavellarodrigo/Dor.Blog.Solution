using Dor.Blog.Domain.Entities;
using Dor.Blog.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using System.Transactions;

namespace Dor.Blog.Infrastructure
{

    public class DbInitializer
    {  

        public async Task InitializeAsync(UserManager<User> _userManager, RoleManager<IdentityRole> _roleManager, DataContext _dbContext)
        {   
            ArgumentNullException.ThrowIfNull(_dbContext, nameof(_dbContext));
            _dbContext.Database.EnsureCreated();

            if (_dbContext.Users.Any()) 
                return;

            User user = new User
            {
                Name = "Rodrigo",
                LastName = "Tavella",
                Status = 2,
                UserName = "rodrigo.tavella",
                Email = "rodrigo@gmail.com",
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
        }
    }   
}
