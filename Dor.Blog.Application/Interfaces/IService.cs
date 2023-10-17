namespace Dor.Blog.Application.Interfaces
{
    public interface IService <T> where T : class
    {
        Task<IEnumerable<T>> GetAsync();

        Task<T> GetByIdAsync(int id);

        Task<int> CreateAsync(T obj);

        public Task<bool> UpdateAsync(T post);

        Task<bool> DeleteAsync(int id);        
    }
}
