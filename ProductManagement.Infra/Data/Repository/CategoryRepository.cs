using Microsoft.EntityFrameworkCore;
using ProductManagement.Domain.Contracts.Repository;
using ProductManagement.Domain.Entities;
using ProductManagement.Infra.Data.Config.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductManagement.Domain.Dtos.Responses;

namespace ProductManagement.Infra.Data.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _dbContext;

        public CategoryRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CreateAsync(Category entity)
        {
            await _dbContext.Categories.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _dbContext.Categories.FindAsync(id);
            if (category != null)
            {
                _dbContext.Categories.Remove(category);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<Category?> GetAsync(int id)
        {
            return await _dbContext.Categories.FindAsync(id);
        }

        public async Task<IEnumerable<Category>> GetAsync()
        {
            return await _dbContext.Categories.ToListAsync();
        }

        public async Task<PaginatedResult<Category>> GetPaginatedAsync(int page, int pageSize)
        {
            var query = _dbContext.Categories.AsQueryable();

            var totalCount = await query.CountAsync();
            var paginatedData = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedResult<Category>
            {
                Items = paginatedData,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize
            };
        }

        public async Task UpdateAsync(Category entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdatePartiallyAsync(Category input)
        {
            var category = await _dbContext.Categories.FindAsync(input.Id);

            if (category != null)
            {
                if(!string.IsNullOrEmpty(input.Description))
                    category.Description = input.Description;
                
                if (input.IsActive != category.IsActive)
                    category.IsActive = input.IsActive;

                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
