
namespace ProductManagement.Domain.Contracts.Services.Base
{
    public interface IGenericWriteService<T, PartialT>
        where T: class
        where PartialT : class
    {
        Task<int> CreateAsync(T entity);
        Task UpdateAsync(int entityId, T entity);
        Task UpdatePartiallyAsync(int entityId, PartialT entity);
        Task DeleteAsync(int id);

    }
}
