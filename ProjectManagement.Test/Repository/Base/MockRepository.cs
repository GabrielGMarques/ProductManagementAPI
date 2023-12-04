using ProductManagement.Domain.Contracts.Repository.Base;
using ProductManagement.Domain.Dtos.Responses;
using ProductManagement.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Test.Repository.Base
{
    public class MockRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly List<T> _data = new List<T>();

        public Task<T?> GetAsync(int id)
        {
            var entity = _data.FirstOrDefault(e => e.Id == id);
            return Task.FromResult(entity);
        }

        public Task<IEnumerable<T>> GetAsync()
        {
            return Task.FromResult<IEnumerable<T>>(_data);
        }

        public Task<int> CreateAsync(T entity)
        {
            _data.Add(entity);
            entity.Id = GenerateUniqueId();
            return Task.FromResult(entity.Id);
        }

        public Task UpdateAsync(T entity)
        {
            var existingEntity = _data.FirstOrDefault(e => e.Id == entity.Id);
            if (existingEntity != null)
            {
                _data.Remove(existingEntity);
                _data.Add(entity);
            }

            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            var entity = _data.FirstOrDefault(e => e.Id == id);
            if (entity != null)
            {
                _data.Remove(entity);
            }

            return Task.CompletedTask;
        }

        public Task<PaginatedResult<T>> GetPaginatedAsync(int page, int pageSize)
        {
            var paginatedData = _data.Skip((page - 1) * pageSize).Take(pageSize);
            var totalItems = _data.Count;
            var result = new PaginatedResult<T>()
            {
                Items = paginatedData,
                TotalCount = totalItems,
                Page = page,
                PageSize = pageSize
            };
            return Task.FromResult(result);
        }

        private int GenerateUniqueId()
        {
            return new Random().Next(1, 1000);
        }

        public Task UpdatePartiallyAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
