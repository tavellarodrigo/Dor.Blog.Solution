using Dor.Blog.Application.Interfaces;
using Dor.Blog.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;
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


        public async Task<BaseResponse<IEnumerable<User>>> GetAllAsync()
        {
            var users =  await _unitOfWork.UserRepository.GetAll();
            return new BaseResponse<IEnumerable<User>>(users);
        }

        public async Task<User?> SingleOrDefaultAsync(Expression<Func<User, bool>> predicate)
        {
            return await _unitOfWork.UserRepository.SingleOrDefaultAsync(predicate);
        }
    }
}
