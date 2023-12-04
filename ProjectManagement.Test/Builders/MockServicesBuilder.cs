using Microsoft.Extensions.Configuration;
using ProductManagement.Application.Config.Mapping;
using ProductManagement.Application.Services;
using ProductManagement.Domain.Contracts.Services;
using ProductManagement.Infra.Data.Repositories;

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


        public static IAuthService BuildAuthService()
        {
            var mockRepository = new MockUserRepository();
            var mapper = AutoMapperConfiguration.Configure().CreateMapper();

            var configuration = new ConfigurationBuilder()
             .AddInMemoryCollection(new Dictionary<string, string?>
             {
                { "JwtSettings:Secret", "this is my custom Secret key for authentication" },
                { "JwtSettings:ExpirationHours", "1" }
             })
             .Build();

            return new AuthService(mockRepository, mapper, configuration);
        }
    }
}
