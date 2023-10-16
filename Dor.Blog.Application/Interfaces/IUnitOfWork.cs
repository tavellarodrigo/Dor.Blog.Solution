using Dor.Blog.Application.Authorization;
using Dor.Blog.Domain.Entities;

namespace Dor.Blog.Application.Interfaces
{
    // En el proyecto MyProject.Infrastructure
    public interface IUnitOfWork 
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
        Task<User> Authenticate(Credential credential);
        

        IUserRepository UserRepository { get; }
        IAuthenticationRepository AuthenticationRepository { get; }

        // Otros métodos relacionados con la gestión de transacciones y la persistencia
    }

}
