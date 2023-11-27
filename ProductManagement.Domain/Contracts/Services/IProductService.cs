﻿using ProductManagement.Domain.Contracts.Services.Base;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Dtos;

namespace ProductManagement.Domain.Contracts.Services
{
    public interface IProductService : IGenericService<ProductDto>
    {

    }
}