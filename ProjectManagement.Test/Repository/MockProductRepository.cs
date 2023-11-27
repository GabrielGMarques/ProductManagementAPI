using ProductManagement.Domain.Contracts.Repository;
using ProductManagement.Domain.Entities;
using ProjectManagement.Test.Repository.Base;

namespace ProductManagement.Infra.Data.Repository
{
    public class MockProductRepository : MockRepository<Product>, IProductRepository
    {
        
    }
}
