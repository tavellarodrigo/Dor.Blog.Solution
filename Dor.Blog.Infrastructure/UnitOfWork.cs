using Dor.Blog.Application.Authorization;
using Dor.Blog.Application.Interfaces;
using Dor.Blog.Domain.Entities;
using Dor.Blog.Infrastructure.Repositories;
using Generic.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace Dor.Blog.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        
        private readonly DataContext _context;
        //private ClientRepository _clientRepository;

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private UserRepository _userRepository;
        private AuthenticationRepository _authenticationRepository;


        public UnitOfWork(DataContext context, RoleManager<IdentityRole> roleManager, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this._context = context;
            this._roleManager = roleManager;
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        public IUserRepository UserRepository
        {
            get
            {
                return _userRepository = _userRepository ?? new UserRepository(_userManager, _context);
            }
        }

        public IAuthenticationRepository AuthenticationRepository
        {
            get
            {
                return _authenticationRepository = _authenticationRepository ?? new AuthenticationRepository(_signInManager, _context);
            }
        }

        public async Task<User> Authenticate(Credential credential)
        {         
            return new User();
        }

        public void BeginTransaction()
        {
            // Iniciar transacción
        }

        public void Commit()
        {
            // Confirmar transacción
        }

        public void Rollback()
        {
            // Revertir transacción
        }

        // Otros métodos relacionados con la gestión de transacciones y la persistencia
    }

}
