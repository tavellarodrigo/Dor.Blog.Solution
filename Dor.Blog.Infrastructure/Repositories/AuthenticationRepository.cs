using Dor.Blog.Application.Interfaces;
using Dor.Blog.Domain.Entities;
using Dor.Blog.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Generic.Data.Repositories
{
    public class AuthenticationRepository : Repository<User>, IAuthenticationRepository
    {
        private readonly SignInManager<User> _signInManager;

        private readonly DataContext _context;
        public AuthenticationRepository(SignInManager<User> signInManager, DataContext context)
        : base(context)
        {
            _signInManager = signInManager;
            _context = context;
        }

        public async Task<SignInResult> CheckPasswordSignIn(User user, string password)
        {

            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);


            return result;

        }
    }
}


