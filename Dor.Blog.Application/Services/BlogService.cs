using Dor.Blog.Application.Authorization;
using Dor.Blog.Application.Interfaces;
using Dor.Blog.Domain.Entities;

namespace Dor.Blog.Application.Services
{
    public class BlogService : IBlogService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperUtil _mapper;

        public BlogService(IUnitOfWork unitOfWork, IMapperUtil mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<BlogPost>> CreateAsync(BlogPost entity)
        {
            await _unitOfWork.BlogRepository.AddAsync(entity);
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
