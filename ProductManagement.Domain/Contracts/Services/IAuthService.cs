using ProductManagement.Domain.Contracts.Services.Base;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Dtos;
using ProductManagement.Domain.Responses;

namespace ProductManagement.Domain.Contracts.Services
{
    public interface IAuthService 
    {
        Task<ServiceResponse<UserDto?>> GetAsync(int id);
        Task<ServiceResponse<string?>> Login(UserDto user);
        Task<ServiceResponse<int?>> Register(UserDto user);
    }
}
