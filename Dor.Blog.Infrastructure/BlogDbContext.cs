using Dor.Blog.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dor.Blog.Infrastructure
{
    public class BlogDbContext : IdentityDbContext<User>
    {
        public DbSet<BlogPost> BlogPosts { get; set; }

        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
        {

        }
    }
}
