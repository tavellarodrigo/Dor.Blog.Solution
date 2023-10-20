using Dor.Blog.Application.Authorization;
using Dor.Blog.Application.Behaviors;
using Dor.Blog.Application.Interfaces;
using Dor.Blog.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Dor.Blog.Application.Services
{
    public class BlogService : IBlogService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperUtil _mapper;
        private readonly IUserService _userService;
        private readonly ILogger<BlogService> _logger;

        public BlogService(IUnitOfWork unitOfWork, 
            IMapperUtil mapper, 
            IUserService userService,
            ILogger<BlogService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userService = userService; 
            _logger = logger;
        }

        public async Task<BaseResponse<BlogPost>> CreateAsync(BlogPost entity)
        {
            _logger.LogInformation(String.Join(" ", "starting to create a post: ", entity));

            //assigning the user to the new post to avoid creating a new user due to FK
            var user = await _userService.SingleOrDefaultAsync(c => c.Id == entity.UserId);            
            if (user == null)
            {
                _logger.LogInformation(String.Join(" ", "User not found. User Id: ", entity.UserId));
                return new BaseResponse<BlogPost>(null,false, "UserId not found: " + entity.UserId);
            }

            entity.User = user??new User();

            //ready for the new post insert
            await _unitOfWork.BlogRepository.AddAsync(entity);

            //save to DB
            await _unitOfWork.SaveAsync();

            return new BaseResponse<BlogPost>(entity);
        } 

        public async Task<BaseResponse<IEnumerable<BlogPost>>> GetAsync()
        {
            var posts = await _unitOfWork.BlogRepository.GetAllAsync();
            return new BaseResponse<IEnumerable<BlogPost>>(posts);
        }

        public async Task<BaseResponse<BlogPost>> GetByIdAsync(int id)
        {
            var post = await _unitOfWork.BlogRepository.GetByIdAsync(id);           

            return new BaseResponse<BlogPost>(post);
        }

        public async Task<BaseResponse<BlogPost>> UpdateAsync(int id, BlogPost updatedPost)
        {
            var postForUpdate = await _unitOfWork.BlogRepository.GetByIdAsync(id);
            if (postForUpdate == null)
            {
                return new BaseResponse<BlogPost>(updatedPost, false, String.Join(" ", "Id Not Found: ", updatedPost.Id));
            }

            //to do use mapping
            postForUpdate.Title = updatedPost.Title;
            postForUpdate.Content = updatedPost.Content;

            await _unitOfWork.SaveAsync();

            return new BaseResponse<BlogPost>(postForUpdate);
        }     

        public async Task<BaseResponse<BlogPost>> DeleteAsync(int id)
        {
            var post = await _unitOfWork.BlogRepository.GetByIdAsync(id);
            if (post == null)
            {
                return new BaseResponse<BlogPost>(null, false, String.Join(" ", "Id Not Found: ", id));
            }
            
            _unitOfWork.BlogRepository.Remove(post);
            
            await _unitOfWork.SaveAsync();

            return new BaseResponse<BlogPost>(post);
        }

    }
}
