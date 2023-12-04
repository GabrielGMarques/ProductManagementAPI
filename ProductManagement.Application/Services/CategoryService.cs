using AutoMapper;
using ProductManagement.Domain.Contracts.Repository;
using ProductManagement.Domain.Contracts.Services;
using ProductManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductManagement.Domain.Dtos.CRUD;
using ProductManagement.Domain.Contracts.Services.Base;

namespace ProductManagement.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _repository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CategoryDto> GetAsync(int id)
        {
            var category = await _repository.GetAsync(id);
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<IEnumerable<CategoryDto>> GetAsync()
        {
            var categories = await _repository.GetAsync();
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public async Task<int> CreateAsync(CategoryDto entity)
        {
            var category = _mapper.Map<Category>(entity);
            return await _repository.CreateAsync(category);
        }

        public async Task UpdateAsync(CategoryDto entity)
        {
            var category = _mapper.Map<Category>(entity);
            await _repository.UpdateAsync(category);
        }
        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<PaginatedResultDto<CategoryDto>> GetPaginatedAsync(int page, int pageSize)
        {
            var paginatedResult = await _repository.GetPaginatedAsync(page, pageSize);
            var mappedData = _mapper.Map<IEnumerable<CategoryDto>>(paginatedResult.Items);

            return new PaginatedResultDto<CategoryDto>
            {
                Items = mappedData,
                TotalCount = paginatedResult.TotalCount,
                Page = paginatedResult.Page,
                PageSize = paginatedResult.PageSize
            };
        }
    }
}
