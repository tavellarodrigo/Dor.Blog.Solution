using Dor.Blog.Application.Interfaces;
using Dor.Blog.Domain.Entities;
using System.Net.WebSockets;

namespace Dor.Blog.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }        

        public async Task<BaseResponse<User>> CreateAsync(User user, string password)
        {            
            await _unitOfWork.UserRepository.CreateUser(user, password);            
            return new BaseResponse<User>(user);
        }
    }
}
