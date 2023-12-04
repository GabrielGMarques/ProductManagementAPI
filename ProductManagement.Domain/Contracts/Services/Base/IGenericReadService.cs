
using ProductManagement.Domain.Dtos.Responses;

namespace ProductManagement.Domain.Contracts.Services.Base
{
    public interface IGenericReadService<T> where T : class
    {
        Task<T> GetAsync(int id);
        Task<IEnumerable<T>> GetAsync();
        Task<PaginatedResult<T>> GetPaginatedAsync(int page, int pageSize);
    }
}
