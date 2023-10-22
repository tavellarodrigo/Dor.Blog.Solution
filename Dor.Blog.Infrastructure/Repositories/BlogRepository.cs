using Dor.Blog.Application.Interfaces;
using Dor.Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Dor.Blog.Infrastructure.Repositories
{
    /// <summary>
    /// Use Identity and Entity Framework
    /// </summary>
    public class BlogRepository : IBlogRepository
    {        
        private readonly DataContext _context;

        public BlogRepository(DataContext context) 
        {        
            _context = context;
        }

        public async Task AddAsync(BlogPost entity)
        {
            await _context.AddAsync(entity);
            
        }

        public async Task<IEnumerable<BlogPost>?> GetAllAsync()
        {
            return await _context.BlogPosts.Where(i => !i.Deleted).ToListAsync();
        }


        public async Task<BlogPost?> GetByIdAsync(int id)
        {
            return await _context.BlogPosts.Include("User").FirstOrDefaultAsync(i => i.Id == id);
        }

        public void Remove(BlogPost entity)
        {
            _context.BlogPosts.Remove(entity);
        }

        public Task AddRangeAsync(IEnumerable<BlogPost> entities)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BlogPost>> Find(Expression<Func<BlogPost, bool>> predicate)
        {
            throw new NotImplementedException();
        }              

        public Task RemoveRange(IEnumerable<BlogPost> entities)
        {
            throw new NotImplementedException();
        }   

        public Task<BlogPost> SingleOrDefaultAsync(Expression<Func<BlogPost, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
