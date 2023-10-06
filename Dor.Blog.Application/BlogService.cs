using Dor.Blog.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dor.Blog.Application
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;

        public BlogService(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public Task<IEnumerable<BlogPost>> GetPostsAsync()
        {
            return _blogRepository.GetPostsAsync();
        }

        public Task<BlogPost> GetPostByIdAsync(int id)
        {
            return _blogRepository.GetPostByIdAsync(id);
        }

        public Task<int> CreatePostAsync(BlogPost post)
        {
            return _blogRepository.CreatePostAsync(post);
        }

        public Task<bool> UpdatePostAsync(BlogPost post)
        {
            return _blogRepository.UpdatePostAsync(post);
        }

        public Task<bool> DeletePostAsync(int id)
        {
            return _blogRepository.DeletePostAsync(id);
        }
    }
}
