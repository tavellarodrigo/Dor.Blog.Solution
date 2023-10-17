using Dor.Blog.Application.Authorization;
using Dor.Blog.Application.Interfaces;
using Dor.Blog.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Dor.Blog.API.Controllers
{
    [Route("api/[controller]")]       
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;        
        private readonly IMapperUtil _mapper;
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(
            UserManager<User> userManager, 
            SignInManager<User> signInManager,
            IMapperUtil mapper, 
            IAuthenticationService authenticationService
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _authenticationService = authenticationService;
        }  

        [HttpPost]
        public async Task<IActionResult> Authenticate(CredentialDTO credentialDTO)
        {
            var credential = _mapper.Map<CredentialDTO, Credential>(credentialDTO);

            var user = await _authenticationService.Authenticate(credential);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
    }
}
