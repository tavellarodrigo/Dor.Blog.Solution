using Dor.Blog.Application.Interfaces;
using Dor.Blog.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Transactions;

namespace Dor.Blog.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly UserManager<User> _userManager;

        private readonly DataContext _context;

        public UserRepository(UserManager<User> userManager,DataContext context)
        : base(context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<User> CreateUser(User User, String Password)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var result = await _userManager.CreateAsync(User, Password);
                if (result.Succeeded)
                {                    
                    await _userManager.AddToRolesAsync(User, User.RoleNames);
                }
                scope.Complete();
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

        //public async Task<User> GetUserById(int id)
        //{
        //    var userFrom = await _context.Users.AsNoTracking()
        //    .Select(user => new User
        //    {
        //        Id = user.Id,
        //        UserName = user.UserName,
        //        Name = user.Name,
        //        LastName = user.LastName
        //        ,
        //        SecurityStamp = user.SecurityStamp
        //        ,
        //        PasswordHash = user.PasswordHash
        //        ,
        //        Status = user.Status
        //    })
        //    .FirstOrDefaultAsync(user => int.Parse(user.Id) == id);

        //    return userFrom;

        //}
        //public async Task<IEnumerable<User>> GetUsers()
        //{
        //    //var roles = _userManager.GetRolesAsync()
        //    var users = await _userManager.Users.AsNoTracking()
        //    .Select(user => new User
        //    {
        //        Id = user.Id,
        //        UserName = user.UserName,
        //        Name = user.Name,
        //        LastName = user.LastName
        //        ,
        //        SecurityStamp = user.SecurityStamp
        //        ,
        //        Status = user.Status
        //    }).ToListAsync();

        //    return users;
        //}

        //public async Task<User> GetUserRoles(User user)
        //{
        //    user.RoleNames = await _userManager.GetRolesAsync(user);
        //    return user;
        //}
        //public async Task<User> AddUserRoles(User user, IEnumerable<string> rolesForAdd, IEnumerable<string> rolesForExclude)
        //{
        //    await _userManager.AddToRolesAsync(user, rolesForAdd.Except(rolesForExclude));
        //    return user;
        //}
        //public async Task<User> RemoveUserRoles(User user, IEnumerable<string> rolesForRemove, IEnumerable<string> rolesForExclude)
        //{
        //    await _userManager.RemoveFromRolesAsync(user, rolesForRemove.Except(rolesForExclude));
        //    return user;
        //}
        //public async Task<User> UpdateUser(User userForBeUpdated)
        //{
        //    IdentityResult result = await _userManager.UpdateAsync(userForBeUpdated);

        //    //var userFromRepo = await GetUserById(userForBeUpdated.Id);

        //    return userForBeUpdated;
        //}

        //public async Task<User> ResetPassword(User user, string newPassword)
        //{
        //    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        //    var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

        //    return user;
        //}
    }
}

