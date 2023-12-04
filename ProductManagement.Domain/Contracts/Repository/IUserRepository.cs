using ProductManagement.Domain.Contracts.Repositories.Base;
using ProductManagement.Domain.Entities;

namespace ProductManagement.Domain.Contracts.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<bool> UserExists(string username);
        Task<User?> GetUserByName(string username);
    }
}
