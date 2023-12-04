using ProductManagement.Domain.Contracts.Repositories;
using ProductManagement.Domain.Entities;
using ProjectManagement.Test.Repository.Base;

namespace ProductManagement.Infra.Data.Repositories
{
    public class MockCategoryRepository : MockRepository<Category>, ICategoryRepository
    {
       
    }
}
