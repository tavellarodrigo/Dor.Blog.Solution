using Dor.Blog.Application.Behaviors;
using Dor.Blog.Application.DTO;
using Dor.Blog.Application.Interfaces;
using Dor.Blog.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dor.Blog.API.Controllers
{
    [Authorize(Roles ="Admin")]    
    [Route("api/[controller]")]    
    [ApiController]
    public class UserController : ControllerBase
    {        
        private readonly IMapperUtil _mapper;
        private readonly IUserService _userService;
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IValidator<UserDTO> _validator;

        public UserController(IMapperUtil mapper, 
            IUserService userService, 
            ILogger<AuthenticationController> logger,
              IValidator<UserDTO> validator)
        {
            
            _mapper = mapper;
            _userService = userService;
            _logger = logger;
            _validator = validator;
        }

        /// <summary>
        /// create a User 
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync(UserDTO userDTO)
        {
            _logger.LogInformation(String.Join(" ", LogMessages.Started, " User: ", userDTO.UserName));


            var resultValidation = _validator.Validate(userDTO);
            if (!resultValidation.IsValid)
            {
                return BadRequest(resultValidation.Errors);
            }


            var user = _mapper.Map<UserDTO, User>(userDTO);
            user.RoleNames = new List<string>
            {
                userDTO.RolName
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

        /// <summary>
        /// get all users 
        /// </summary>
        /// <returns></returns>
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
