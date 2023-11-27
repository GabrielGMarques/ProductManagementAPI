using Microsoft.EntityFrameworkCore;
using ProductManagement.Domain.Contracts.Repository;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Dtos;
using ProductManagement.Infra.Data.Config.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Infra.Data.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _dbContext; // Replace YourDbContext with the actual name of your DbContext

        public CategoryRepository(DataContext dbContext) // Replace YourDbContext with the actual name of your DbContext
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

        public Task<PaginatedResultDto<Category>> GetPaginatedAsync(int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Category entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
