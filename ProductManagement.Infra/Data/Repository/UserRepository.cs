using Microsoft.EntityFrameworkCore;
using ProductManagement.Domain.Contracts.Repository;
using ProductManagement.Domain.Entities;
using ProductManagement.Infra.Data.Config.Context;
using ProductManagement.Domain.Shared.Responses;

namespace ProductManagement.Infra.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dbContext;

        public UserRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User?> GetAsync(int id)
        {
            return await _dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<User>> GetAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<int> CreateAsync(User entity)
        {
            _dbContext.Users.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity.Id;
        }
        public async Task UpdateAsync(User input)
        {
            var user = await _dbContext.Users.FindAsync(input.Id);

            if (user != null)
            {
                _dbContext.Entry(input).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }

            throw new Exception("User not found");
        }

        public async Task DeleteAsync(int id)
        {
            var userToDelete = await _dbContext.Users.FindAsync(id);

            if (userToDelete != null)
            {
                _dbContext.Users.Remove(userToDelete);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<bool> UserExists(string username)
        {
            return await _dbContext.Users.AnyAsync(u => u.Username.ToLower() == username.ToLower());
        }

        public async Task<User?> GetUserByName(string username)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());
        }

        public Task<PaginatedResult<User>> GetPaginatedAsync(int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUniqueAsync(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
