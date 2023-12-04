using Microsoft.EntityFrameworkCore;
using ProductManagement.Domain.Contracts.Repository;
using ProductManagement.Domain.Entities;
using ProductManagement.Infra.Data.Config.Context;
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
                .AsNoTracking()
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
    
    }
}
