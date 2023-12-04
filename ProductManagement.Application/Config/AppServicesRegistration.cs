using Microsoft.Extensions.DependencyInjection;
using ProductManagement.Application.Config.Mapping;
using ProductManagement.Application.Services;
using ProductManagement.Domain.Contracts.Services;

namespace ProductManagement.Application.Config
{
    public static class AppServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Add services
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IAuthService, AuthService>();

            // Add AutoMapper
            var mapper = AutoMapperConfiguration.Configure().CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }

    }
}
