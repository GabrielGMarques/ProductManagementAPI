using AutoMapper;
using ProductManagement.Domain.Contracts.Repository;
using ProductManagement.Domain.Contracts.Services;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Task<PaginatedResultDto<ProductDto>> GetPaginatedAsync(int page, int pageSize)
        {
            throw new NotImplementedException();
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

    }
}
