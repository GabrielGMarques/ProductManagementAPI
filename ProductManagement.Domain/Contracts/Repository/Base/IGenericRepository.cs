using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductManagement.Domain.Entities.Base;

namespace ProductManagement.Domain.Contracts.Repository.Base
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T?> GetAsync(int id);
        Task<IEnumerable<T>> GetAsync();
        Task<int> CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task<PaginatedResultDto<T>> GetPaginatedAsync(int page, int pageSize);

    }
}
