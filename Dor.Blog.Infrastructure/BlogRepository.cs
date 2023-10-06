using Dor.Blog.Domain;
using Microsoft.EntityFrameworkCore;

namespace Dor.Blog.Infrastructure
{
    public class BlogRepository : IBlogRepository
    {
        private readonly BlogDbContext _dbContext;

        public BlogRepository(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<BlogPost>> GetPostsAsync()
        {
            return await _dbContext.BlogPosts.ToListAsync();
        }

        public async Task<BlogPost> GetPostByIdAsync(int id)
        {
            return await _dbContext.BlogPosts.FindAsync(id)??new BlogPost();
        }

        public async Task<int> CreatePostAsync(BlogPost post)
        {
            _dbContext.BlogPosts.Add(post);
            await _dbContext.SaveChangesAsync();
            return post.Id;
        }

        public async Task<bool> UpdatePostAsync(BlogPost post)
        {
            _dbContext.Entry(post).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(post.Id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        public async Task<bool> DeletePostAsync(int id)
        {
            var post = await _dbContext.BlogPosts.FindAsync(id);
            if (post == null)
            {
                return false;
            }

            _dbContext.BlogPosts.Remove(post);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        private bool PostExists(int id)
        {
            return _dbContext.BlogPosts.Any(e => e.Id == id);
        }
    }
}
