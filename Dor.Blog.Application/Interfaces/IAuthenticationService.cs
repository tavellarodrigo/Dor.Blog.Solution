using Dor.Blog.Application.Authorization;
using Dor.Blog.Domain.Entities;

namespace Dor.Blog.Application.Interfaces
{
    public interface IAuthenticationService
    {
        Task<User> Authenticate(Credential credential);
    }
}
