using ProductManagement.Domain.Entities.Base;
using ProductManagement.Domain.Shared.Responses;

namespace ProductManagement.Domain.Contracts.Repository.Base
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T?> GetAsync(int id);
        Task<IEnumerable<T>> GetAsync();
        Task<int> CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task<PaginatedResult<T>> GetPaginatedAsync(int page, int pageSize);
    }
}
