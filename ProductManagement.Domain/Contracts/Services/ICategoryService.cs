using ProductManagement.Domain.Contracts.Services.Base;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Dtos.CRUD;

namespace ProductManagement.Domain.Contracts.Services
{
    public interface ICategoryService : IGenericReadService<CategoryDto>, IGenericWriteService<CategoryDto>
    {
    }
}
