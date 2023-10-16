using AutoMapper;
using Dor.Blog.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dor.Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Policy = "RequireAdministratorRole")]
    [Authorize]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;
        private readonly IMapper _mapper;

        public BlogController(IBlogService blogService, IMapper mapper)
        {
            _blogService = blogService;
            _mapper = mapper;   
        }



        //[HttpGet]        
        //public async Task<ActionResult<IEnumerable<BlogPost>>> Get()
        //{
        //    var posts = await _blogService.GetPostsAsync();
        //    return Ok(posts);
        //}

        //[HttpGet("{id}")]
        //public async Task<ActionResult<BlogPost>> Get(int id)
        //{
        //    var post = await _blogService.GetPostByIdAsync(id);

        //    if (post == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(post);
        //}

        //[HttpPost]
        //public async Task<ActionResult<BlogPost>> Post([FromBody] BlogPost newPost)
        //{
        //    var postId = await _blogService.CreatePostAsync(newPost);
        //    newPost.Id = postId;
        //    return CreatedAtAction(nameof(Get), new { id = postId }, newPost);
        //}

        //[HttpPut("{id}")]
        //public async Task<IActionResult> Put(int id, [FromBody] BlogPost updatedPost)
        //{
        //    var success = await _blogService.UpdatePostAsync(updatedPost);

        //    if (!success)
        //    {
        //        return NotFound();
        //    }

        //    return NoContent();
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var success = await _blogService.DeletePostAsync(id);
        //    if (!success)
        //    {
        //        return NotFound();
        //    }

        //    return NoContent();

        //}
    }    
}
        
