using AutoMapper;
using Dor.Blog.Application.DTO;
using Dor.Blog.Application.Interfaces;
using Dor.Blog.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace Dor.Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;
        private readonly IMapper _mapper;

        public BlogController(IBlogService blogService, IMapper mapper)
        {
            _blogService = blogService;
            _mapper = mapper;   
        }

        [HttpPost]
        public async Task<ActionResult<BlogPost>> Post([FromBody] PostForCreateDTO post)
        {
            var newPost = _mapper.Map<PostForCreateDTO, BlogPost>(post);

            var result = await _blogService.CreateAsync(newPost);               
            if (!result.Successful)
                return NotFound();

            return CreatedAtAction(nameof(Post), new { id = newPost.Id }, newPost);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostDTO>>> GetAll()
        {
            var result = await _blogService.GetAsync();
            if (!result.Successful)
                return BadRequest(result.errors);

            return Ok(result.DataResponse);
        }

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

        [HttpGet("{id}")]
        public async Task<ActionResult<BlogPost>> Get(int id)
        {
            var result = await _blogService.GetByIdAsync(id);

            if (!result.Successful)
            {
                return NotFound(result.DataResponse);
            }

            return Ok(result.DataResponse);
        }

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
        
