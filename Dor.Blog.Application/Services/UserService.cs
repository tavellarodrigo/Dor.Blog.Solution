using Dor.Blog.Application.Interfaces;
using Dor.Blog.Domain.Entities;
using System.Linq.Expressions;

namespace Dor.Blog.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }        

        /// <summary>
        /// create one user 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<BaseResponse<User>> CreateAsync(User user, string password)
        {            
            await _unitOfWork.UserRepository.CreateUser(user, password);            
            return new BaseResponse<User>(user);
        }

        /// <summary>
        /// get all users
        /// </summary>
        /// <returns></returns>
        public async Task<BaseResponse<IEnumerable<User>>> GetAllAsync()
        {
            var users =  await _unitOfWork.UserRepository.GetAll();
            return new BaseResponse<IEnumerable<User>>(users);
        }

        /// <summary>
        /// get one or null user
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<User?> SingleOrDefaultAsync(Expression<Func<User, bool>> predicate)
        {
            return await _unitOfWork.UserRepository.SingleOrDefaultAsync(predicate);
        }
    }
}
