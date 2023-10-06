using Dor.Blog.Domain;

namespace Dor.Blog.Application
{
    public interface IBlogService
    {
        Task<IEnumerable<BlogPost>> GetPostsAsync();
        Task<BlogPost> GetPostByIdAsync(int id);
        Task<int> CreatePostAsync(BlogPost post);
        Task<bool> UpdatePostAsync(BlogPost post);
        Task<bool> DeletePostAsync(int id);
    }
}
