
using ProductManagement.Domain.Dtos;

namespace ProductManagement.Domain.Contracts.Services.Base
{
    public interface IGenericService<T> where T : class
    {
        Task<T> GetAsync(int id);
        Task<IEnumerable<T>> GetAsync();
        Task<int> CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task<PaginatedResultDto<ProductDto>> GetPaginatedAsync(int page, int pageSize);

    }
}
