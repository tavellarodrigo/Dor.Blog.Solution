using Dor.Blog.Domain.Entities;

namespace Dor.Blog.Application.Interfaces
{
    public interface IService <T> where T : class
    {
        Task<BaseResponse<IEnumerable<T>>> GetAsync();

        Task<BaseResponse<T>> GetByIdAsync(int id);

        Task<BaseResponse<T>> CreateAsync(T entity);

        Task<BaseResponse<T>> UpdateAsync(int id, T entity);

        Task<BaseResponse<T>> DeleteAsync(int id);        
    }
}
