using AutoMapper;
using Dor.Blog.Application.Behaviors;
using Dor.Blog.Application.DTO;
using Dor.Blog.Application.Interfaces;
using Dor.Blog.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Dor.Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {
        private readonly IBlogService _blogService;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IValidator<PostForCreateDTO> _validator;

        public BlogPostController(IBlogService blogService, 
            IMapper mapper, 
            ILogger<AuthenticationController> logger,
            IValidator<PostForCreateDTO> validator)
        {
            _blogService = blogService;
            _mapper = mapper;   
            _logger = logger;
            _validator = validator;

        }

        /// <summary>
        /// Create a post for valid user
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<BlogPost>> Post([FromBody] PostForCreateDTO post)
        {
            _logger.LogInformation(String.Join(" ", LogMessages.Started, " Post: ", post));

            var resultValidation = _validator.Validate(post);
            if (!resultValidation.IsValid)
            {
                return BadRequest(resultValidation.Errors);
            }

            var newPost = _mapper.Map<PostForCreateDTO, BlogPost>(post);

            var result = await _blogService.CreateAsync(newPost);
            if (!result.Successful)
            {
                _logger.LogInformation(LogMessages.NotFound);
                return NotFound(result.errors);
            }

            _logger.LogInformation(LogMessages.Finished);

            return CreatedAtAction(nameof(Post), new { id = newPost.Id }, newPost);
        }

        /// <summary>
        /// get all posts, without filters
        /// TO DO add filters
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostDTO>>> GetAll()
        {
            var result = await _blogService.GetAsync();
            if (!result.Successful)
                return BadRequest(result.errors);

            return Ok(result.DataResponse);
        }

        /// <summary>
        /// update a post
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedPostDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] PostForUpdateDTO updatedPostDTO)
        {
            var updatedPost = _mapper.Map<PostForUpdateDTO, BlogPost>(updatedPostDTO);

            var result = await _blogService.UpdateAsync(id, updatedPost);

            if (!result.Successful)
            {
                return BadRequest(result.errors);
            }

            return Ok(result.DataResponse);
        }

        /// <summary>
        /// get one post by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<BlogPost>> Get(int id)
        {
            var result = await _blogService.GetByIdAsync(id);

            if (!result.Successful || result.DataResponse == null)
            {
                return NotFound(String.Join(" ", "Id not found " + id.ToString()));
            }

            return Ok(result.DataResponse);
        }

        /// <summary>
        /// delete one post by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _blogService.DeleteAsync(id);
            if (!result.Successful)
            {
                return NotFound();
            }

            return Ok();

        }
    }
}
        
