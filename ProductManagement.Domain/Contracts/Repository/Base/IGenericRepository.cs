using ProductManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductManagement.Domain.Entities.Base;
using ProductManagement.Domain.Dtos.Responses;

namespace ProductManagement.Domain.Contracts.Repository.Base
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T?> GetAsync(int id);
        Task<IEnumerable<T>> GetAsync();
        Task<int> CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task UpdatePartiallyAsync(T entity);
        Task DeleteAsync(int id);
        Task<PaginatedResult<T>> GetPaginatedAsync(int page, int pageSize);

    }
}
