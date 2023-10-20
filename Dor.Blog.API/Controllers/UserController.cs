using Dor.Blog.Application.Authorization;
using Dor.Blog.Application.Behaviors;
using Dor.Blog.Application.DTO;
using Dor.Blog.Application.Interfaces;
using Dor.Blog.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Dor.Blog.API.Controllers
{
    [Authorize(Roles ="Admin")]
    [Authorize]
    [Route("api/[controller]")]    
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapperUtil _mapper;
        private readonly IUserService _userService;
        private readonly ILogger<AuthenticationController> _logger;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager, IMapperUtil mapper, IUserService userService, ILogger<AuthenticationController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _userService = userService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(UserDTO userDTO)
        {
            _logger.LogInformation(String.Join(" ", LogMessages.Started, " User: ", userDTO.UserName));

            var user = _mapper.Map<UserDTO, User>(userDTO);
            user.RoleNames = new List<string>
            {
                "Admin"
            };

            string password = userDTO.Password;            
            var result = await _userService.CreateAsync(user, password);
            if (!result.Successful)
            {
                return UnprocessableEntity(result.errors.ToString()); 
            }

            _logger.LogInformation(LogMessages.Finished);

            return Ok(userDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _userService.GetAllAsync();
            if (!result.Successful)
            {
                return NotFound();
            }
            return Ok(result.DataResponse);
        }

    }
}
