using Dor.Blog.Application.Interfaces;
using Dor.Blog.Domain.Entities;

namespace Dor.Blog.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User> CreateAsync(User obj, string password)
        {
            return await _unitOfWork.UserRepository.CreateUser(obj, password);            
        }

        public async Task<int> CreateAsync(User obj)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(User post)
        {
            throw new NotImplementedException();
        }
    }
}
