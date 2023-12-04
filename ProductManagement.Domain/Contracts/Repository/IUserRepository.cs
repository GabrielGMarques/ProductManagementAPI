using ProductManagement.Domain.Contracts.Repository.Base;
using ProductManagement.Domain.Entities;

namespace ProductManagement.Domain.Contracts.Repository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<bool> UserExists(string username);
        Task<User?> GetUserByName(string username);
    }
}
