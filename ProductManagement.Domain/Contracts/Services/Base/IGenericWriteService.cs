
namespace ProductManagement.Domain.Contracts.Services.Base
{
    public interface IGenericWriteService<T>
        where T: class
    {
        Task<int> CreateAsync(T entity);
        Task UpdateAsync(int entityId, T entity);
        Task DeleteAsync(int id);

    }
}
