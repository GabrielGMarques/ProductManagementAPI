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
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _dbContext;

        public ProductRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Product?> GetAsync(int id)
        {
            return await _dbContext.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Product>> GetAsync()
        {
            return await _dbContext.Products
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task<PaginatedResult<Product>> GetPaginatedAsync(int page, int pageSize)
        {
            var query = _dbContext.Products.AsQueryable();

            var totalCount = await query.CountAsync();
            var paginatedData = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedResult<Product>
            {
                Items = paginatedData,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize
            };
        }

        public async Task<int> CreateAsync(Product entity)
        {
            _dbContext.Products.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity.Id;
        }
        public async Task UpdateAsync(Product entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
        
        public async Task DeleteAsync(int id)
        {
            var productToDelete = await _dbContext.Products.FindAsync(id);

            if (productToDelete != null)
            {
                _dbContext.Products.Remove(productToDelete);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateCategoryAsync(int productId, int idCategory)
        {
            var product = await _dbContext.Products
                    .Include(p => p.Category)
                    .FirstAsync(x => x.Id == productId);

            product.CategoryId = idCategory;
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdatePartiallyAsync(Product input)
        {
            var product = await _dbContext.Products.FindAsync(input.Id);

            if (product != null)
            {
                if (!string.IsNullOrEmpty(input.Description))
                    product.Description = input.Description;

                if (input.Description != product.Description)
                    product.Description = input.Description;
                
                if (input.ProductCode != product.ProductCode)
                    product.ProductCode = input.ProductCode;
                
                if (input.ProductReference != product.ProductReference)
                    product.ProductReference = input.ProductReference;

                if (input.Stock != product.Stock)
                    product.Stock = input.Stock;
                
                if (input.Price != product.Price)
                    product.Price = input.Price;
                
                if (input.Width != product.Width)
                    product.Width = input.Width;
                
                if (input.Height != product.Height)
                    product.Height = input.Height;
                
                if (input.Weight != product.Weight)
                    product.Weight = input.Weight;
                
                if (input.IsActive != product.IsActive)
                    product.IsActive = input.IsActive;
                
                if (input.CategoryId != product.CategoryId)
                    product.CategoryId = input.CategoryId;

                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
