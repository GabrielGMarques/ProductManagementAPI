﻿using AutoMapper;
using ProductManagement.Domain.Contracts.Repository;
using ProductManagement.Domain.Contracts.Services;
using ProductManagement.Domain.Contracts.Services.Base;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _repository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductDto> GetAsync(int id)
        {
            var product = await _repository.GetAsync(id);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> GetAsync()
        {
            var products = await _repository.GetAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<PaginatedResultDto<ProductDto>> GetPaginatedAsync(int page, int pageSize)
        {
            var paginatedResult = await _repository.GetPaginatedAsync(page, pageSize);
            var mappedData = _mapper.Map<IEnumerable<ProductDto>>(paginatedResult.Data);

            return new PaginatedResultDto<ProductDto>
            {
                Data = mappedData,
                TotalCount = paginatedResult.TotalCount,
                Page = paginatedResult.Page,
                PageSize = paginatedResult.PageSize
            };
        }

        public async Task<int> CreateAsync(ProductDto entity)
        {
            var product = _mapper.Map<Product>(entity);
            return await _repository.CreateAsync(product);
        }

        public async Task UpdateAsync(ProductDto entity)
        {
            var product = _mapper.Map<Product>(entity);
            await _repository.UpdateAsync(product);
        }
        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

    }
}