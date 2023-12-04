using AutoMapper;
using ProductManagement.Domain.Contracts.Repositories;
using ProductManagement.Domain.Contracts.Services;
using ProductManagement.Domain.Contracts.Services.Base;
using ProductManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductManagement.Domain.Shared.Dtos;
using ProductManagement.Domain.Shared.Responses;

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

        public async Task<ProductReadDto> GetAsync(int id)
        {
            var product = await _repository.GetAsync(id);
            return _mapper.Map<ProductReadDto>(product);
        }

        public async Task<ProductWriteDto> GetWritableDtoAsync(int id)
        {
            var category = await _repository.GetAsync(id);
            return _mapper.Map<ProductWriteDto>(category);
        }

        public async Task<IEnumerable<ProductReadDto>> GetAsync()
        {
            var products = await _repository.GetAsync();
            return _mapper.Map<IEnumerable<ProductReadDto>>(products);
        }

        public async Task<PaginatedResult<ProductReadDto>> GetPaginatedAsync(int page, int pageSize)
        {
            var paginatedResult = await _repository.GetPaginatedAsync(page, pageSize);
            var mappedData = _mapper.Map<IEnumerable<ProductReadDto>>(paginatedResult.Items);

            return new PaginatedResult<ProductReadDto>
            {
                Items = mappedData,
                TotalCount = paginatedResult.TotalCount,
                Page = paginatedResult.Page,
                PageSize = paginatedResult.PageSize
            };
        }

        public async Task<int> CreateAsync(ProductWriteDto entity)
        {
            var product = _mapper.Map<Product>(entity);
            return await _repository.CreateAsync(product);
        }

        public async Task UpdateAsync(int id, ProductWriteDto entity)
        {
            var product = _mapper.Map<Product>(entity);
            product.Id = id;

            await _repository.UpdateAsync(product);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    
    }
}
