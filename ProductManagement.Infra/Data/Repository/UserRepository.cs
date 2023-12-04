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
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dbContext;

        public UserRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetAsync(int id)
        {
            return await _dbContext.Users.FindAsync(id);
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
        public async Task UpdateAsync(User entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
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
    }
}
