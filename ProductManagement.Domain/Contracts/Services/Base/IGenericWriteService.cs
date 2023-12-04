
using ProductManagement.Domain.Dtos;
using ProductManagement.Domain.Dtos.CRUD;

namespace ProductManagement.Domain.Contracts.Services.Base
{
    public interface IGenericWriteService<T> where T : class
    {
        Task<int> CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);

    }
}
