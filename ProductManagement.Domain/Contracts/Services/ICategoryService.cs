using ProductManagement.Domain.Contracts.Services.Base;
using ProductManagement.Domain.Dtos.CRUD;

namespace ProductManagement.Domain.Contracts.Services
{
    public interface ICategoryService : IGenericReadService<CategoryReadDto>,  IGenericWriteService<CategoryWriteDto>
    { }
}
