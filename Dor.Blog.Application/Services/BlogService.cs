using Dor.Blog.Application.Interfaces;
using Dor.Blog.Domain.Entities;

namespace Dor.Blog.Application.Services
{
    public class BlogService : IBlogService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BlogService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<int> CreateAsync(BlogPost obj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BlogPost>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public Task<BlogPost> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(BlogPost post)
        {
            throw new NotImplementedException();
        }


    }
}
