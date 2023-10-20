using Dor.Blog.Application.Interfaces;
using Dor.Blog.Domain.Entities;
using Dor.Blog.Infrastructure.Repositories;
using Generic.Data.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Dor.Blog.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        
        private readonly DataContext _context;        
        
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private UserRepository _userRepository;
        private AuthenticationRepository _authenticationRepository;
        private BlogRepository _blogRepository;

        public UnitOfWork(DataContext context, 
            RoleManager<IdentityRole> roleManager, 
            UserManager<User> userManager, 
            SignInManager<User> signInManager)
        {
            this._context = context;            
            this._userManager = userManager;
            this._signInManager = signInManager;
            
        }

        public IUserRepository UserRepository
        {
            get
            {
                return _userRepository = _userRepository ?? new UserRepository(_userManager);
            }
        }

        public IAuthenticationRepository AuthenticationRepository
        {
            get
            {
                return _authenticationRepository = _authenticationRepository ?? new AuthenticationRepository(_signInManager);
            }
        }

        public IBlogRepository BlogRepository
        {
            get
            {
                return _blogRepository = _blogRepository ?? new BlogRepository(_context);
            }
        }

        public void BeginTransaction()
        {
            
        }

        public void Commit()
        {
            
        }

        public void Rollback()
        {
            
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

    }

}
