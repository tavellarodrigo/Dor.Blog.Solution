namespace Dor.Blog.Application.Interfaces
{    
    public interface IUnitOfWork 
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
        Task SaveAsync();

        IUserRepository UserRepository { get; }
        IAuthenticationRepository AuthenticationRepository { get; }
        IBlogRepository BlogRepository { get; }
        
    }

}
