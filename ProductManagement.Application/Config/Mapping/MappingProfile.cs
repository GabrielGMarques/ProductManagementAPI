using AutoMapper;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Dtos.Auth;
using ProductManagement.Domain.Dtos.CRUD;

namespace ProductManagement.Application.Config.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // CRUD
            CreateMap<ProductWriteDto, Product>()
                .ReverseMap();

            CreateMap<ProductReadDto, Product>()
                .ReverseMap()
                .ForMember(dest => dest.CategoryDescription, opt => opt.MapFrom(src => src.Category.Description));
            
            CreateMap<CategoryReadDto, Category>().ReverseMap();
            CreateMap<CategoryWriteDto, Category>().ReverseMap();

            // Auth & User
            CreateMap<LoginDto, User>().ReverseMap();
            CreateMap<RegisterDto, User>().ReverseMap();
            CreateMap<UserReadDto, User>().ReverseMap();
        }
    }
}
