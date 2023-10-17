using Dor.Blog.Application.Authorization;
using Dor.Blog.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Dor.Blog.Application.Interfaces
{
    public interface IAuthenticationRepository 
    {
        Task<User> Authenticate(Credential credential);
        Task<SignInResult> CheckPasswordSignInAsync(User user, string password);
        
    }
}