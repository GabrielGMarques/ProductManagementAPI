using AutoMapper;
using ProductManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductManagement.Domain.Dtos.Auth;
using ProductManagement.Domain.Dtos.CRUD;
using ProductManagement.Domain.Dtos;

namespace ProductManagement.Application.Config.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // CRUD
            CreateMap<ProductCreationDto, Product>()
                .ReverseMap();

            CreateMap<ProductDto, Product>()
                .ReverseMap()
                .ForMember(dest => dest.CategoryDescription, opt => opt.MapFrom(src => src.Category.Description));
            
            CreateMap<CategoryDto, Category>().ReverseMap();
            
            // Auth & User
            CreateMap<LoginDto, User>().ReverseMap();
            CreateMap<RegisterDto, User>().ReverseMap();
            CreateMap<UserDto, User>().ReverseMap();
        }
    }
}
