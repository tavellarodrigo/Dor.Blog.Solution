using Dor.Blog.Application.Authorization;
using Dor.Blog.Application.Interfaces;
using Dor.Blog.Domain.Entities;
using Dor.Blog.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Generic.Data.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly SignInManager<User> _signInManager;        

        public AuthenticationRepository(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;            
        }           

        public async Task<SignInResult> CheckPasswordSignInAsync(User user, string password)
        {
            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);            

            return result;

        }
    }
}


