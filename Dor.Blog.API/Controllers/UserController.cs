using Dor.Blog.Application.DTO;
using Dor.Blog.Application.Interfaces;
using Dor.Blog.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Dor.Blog.API.Controllers
{
    [Authorize(Roles ="Admin")]
    //[Authorize]
    [Route("api/[controller]")]    
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapperUtil _mapper;
        private readonly IUserService _userService;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager, IMapperUtil mapper, IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(UserDTO userDTO)
        {
            
            var user = _mapper.Map<UserDTO, User>(userDTO);
            user.RoleNames = new List<string>
            {
                "Admin"
            };

            string password = userDTO.Password;
            var result = await _userService.CreateAsync(user, password);
            if (result == null)
            {
                return UnprocessableEntity();                

            }

            return Ok(userDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok("All ok");
        }

    }
}
