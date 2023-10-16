using Dor.Blog.Application.Interfaces;
using Dor.Blog.Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dor.Blog.Application.Authorization
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthenticationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User> Authenticate(Credential credential)
        {
            //Search for user filtering by userName
            var user = await _unitOfWork.UserRepository.GetUserByUserName(credential.UserName);

            if (user == null)
            {   
                var error = new InvalidOperationException("User doesn't exists");
                return await Task.FromException<User>(error);

            }

            var res = await _unitOfWork.AuthenticationRepository.CheckPasswordSignIn(user, credential.Password);

            if (!res.Succeeded)
            {
                var error = new InvalidOperationException("Username or Password doesn't match.");
                return await Task.FromException<User>(error);
            }

            //generate token

            return  await _unitOfWork.Authenticate(credential);            
        }

    }
}
