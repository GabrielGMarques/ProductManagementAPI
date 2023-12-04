using ProductManagement.Domain.Shared.Responses;
using ProductManagement.Domain.Shared.Dtos;

namespace ProductManagement.Domain.Contracts.Services
{
    public interface IAuthService 
    {
        Task<ServiceResponse<UserReadDto?>> GetAsync(int id);
        Task<ServiceResponse<string?>> Login(LoginDto user);
        Task<ServiceResponse<int?>> Register(RegisterDto user);
    }
}
