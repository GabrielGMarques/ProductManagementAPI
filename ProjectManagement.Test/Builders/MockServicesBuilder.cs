using ProductManagement.Application.Config.Mapping;
using ProductManagement.Application.Services;
using ProductManagement.Domain.Contracts.Services;
using ProductManagement.Infra.Data.Repository;

namespace ProjectManagement.Test.Builders
{
    public static class MockServicesBuilder
    {
        public static IProductService BuildProductService()
        {
            var mockRepository = new MockProductRepository();
            var mapper = AutoMapperConfiguration.Configure().CreateMapper();
            return new ProductService(mockRepository, mapper);
        }

        public static ICategoryService BuildCategoryService()
        {
            var mockRepository = new MockCategoryRepository();
            var mapper = AutoMapperConfiguration.Configure().CreateMapper();
            return new CategoryService(mockRepository, mapper);
        }
    }
}
