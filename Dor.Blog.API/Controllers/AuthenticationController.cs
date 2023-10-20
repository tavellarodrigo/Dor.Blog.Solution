using Dor.Blog.Application.Authorization;
using Dor.Blog.Application.Behaviors;
using Dor.Blog.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Dor.Blog.API.Controllers
{
    [Route("api/[controller]")]       
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMapperUtil _mapper;
        private readonly IAuthenticationService _authenticationService;
        private readonly ILogger<AuthenticationController> _logger;        

        public AuthenticationController(            
            IMapperUtil mapper, 
            IAuthenticationService authenticationService
            ,ILogger<AuthenticationController> logger

            )
        {
            _mapper = mapper;
            _authenticationService = authenticationService;
            _logger = logger;
        }  

        [HttpPost]
        public async Task<IActionResult> Authenticate(CredentialDTO credentialDTO)
        {
            _logger.LogInformation(String.Join(" ",LogMessages.Started," User: ", credentialDTO.UserName));

            var credential = _mapper.Map<CredentialDTO, Credential>(credentialDTO);

            _logger.LogInformation("Mapping completed");

            var result = await _authenticationService.Authenticate(credential);

            if (!result.Successful)
            {
                _logger.LogInformation(LogMessages.NotFound);
                return NotFound();
            }

            _logger.LogInformation(LogMessages.Finished);

            return Ok(result.DataResponse);
        }
    }
}
