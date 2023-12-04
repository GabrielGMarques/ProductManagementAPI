using ProductManagement.Domain.Contracts.Services.Base;
using ProductManagement.Domain.Shared.Dtos;

namespace ProductManagement.Domain.Contracts.Services
{
    public interface IProductService : IGenericReadService<ProductReadDto>, IGenericWriteService<ProductWriteDto>
    { }
}
