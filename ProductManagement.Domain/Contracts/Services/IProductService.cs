using ProductManagement.Domain.Contracts.Services.Base;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Dtos.CRUD;

namespace ProductManagement.Domain.Contracts.Services
{
    public interface IProductService : IGenericReadService<ProductReadDto>, IGenericWriteService<ProductWriteDto>
    {
        Task UpdateCategoryAsync(int idCategory);
    }
}
