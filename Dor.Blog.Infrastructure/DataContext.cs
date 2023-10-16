using Dor.Blog.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dor.Blog.Infrastructure.Repositories
{
    public class DataContext : IdentityDbContext<User>
    {
        public DbSet<BlogPost> BlogPosts { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
    }
}
