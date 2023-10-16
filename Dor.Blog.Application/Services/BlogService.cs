using Dor.Blog.Application.Authorization;
using Dor.Blog.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Dor.Blog.Application.Services
{
    public class BlogService : IBlogService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BlogService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

      


        //public Task<IEnumerable<BlogPost>> GetPostsAsync()
        //{
        //    return _blogRepository.GetPostsAsync();
        //}

        //public Task<BlogPost> GetPostByIdAsync(int id)
        //{
        //    return _blogRepository.GetPostByIdAsync(id);
        //}

        //public Task<int> CreatePostAsync(BlogPost post)
        //{
        //    return _blogRepository.CreatePostAsync(post);
        //}

        //public Task<bool> UpdatePostAsync(BlogPost post)
        //{
        //    return _blogRepository.UpdatePostAsync(post);
        //}

        //public Task<bool> DeletePostAsync(int id)
        //{
        //    return _blogRepository.DeletePostAsync(id);
        //}
    }
}
