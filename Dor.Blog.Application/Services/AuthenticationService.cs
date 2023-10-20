using Dor.Blog.Application.Authorization;
using Dor.Blog.Application.Interfaces;
using Dor.Blog.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Dor.Blog.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;

        public AuthenticationService(IUnitOfWork unitOfWork, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _config = config;

        }
        
        /// <summary>
        /// Get token to create Users
        /// </summary>
        /// <param name="credential"></param>
        /// <returns></returns>
        public async Task<BaseResponse<User>> Authenticate(Credential credential)
        { 
            //look for user
            var user = await _unitOfWork.UserRepository.GetUserByUserName(credential.UserName);

            if (user == null)
            {
                return new BaseResponse<User>(null, false, "User doesn't exists");
            }

            //user exists then check user and password
            var res = await _unitOfWork.AuthenticationRepository.CheckPasswordSignInAsync(user, credential.Password);

            if (!res.Succeeded)
            {                
                return new BaseResponse<User>(null, false, "Username or Password doesn't match.");
            }

            //generate token
            user.token = await GenerateJwtToken(user);

            return new BaseResponse<User>(user);
        }

        /// <summary>
        /// Generate token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private async Task<string> GenerateJwtToken(User user)
        {
            string secretKey = _config.GetSection("JWT:SecretKey").Value ?? "";

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName??"")
            };

            foreach (var role in user.RoleNames){
                claims.Add(new Claim(ClaimTypes.Role, user.RoleNames.FirstOrDefault() ?? ""));
            }
            

            // generate the JWT
            var jwt = new JwtSecurityToken(
                    claims: claims,
                    notBefore: DateTime.Now,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                        SecurityAlgorithms.HmacSha256Signature)
                );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }     

    }
}
