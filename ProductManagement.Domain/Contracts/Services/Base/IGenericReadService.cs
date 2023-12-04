
using ProductManagement.Domain.Dtos;
using ProductManagement.Domain.Dtos.CRUD;

namespace ProductManagement.Domain.Contracts.Services.Base
{
    public interface IGenericReadService<T> where T : class
    {
        Task<T> GetAsync(int id);
        Task<IEnumerable<T>> GetAsync();
        Task<PaginatedResultDto<T>> GetPaginatedAsync(int page, int pageSize);

    }
}
