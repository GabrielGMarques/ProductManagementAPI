using AutoMapper;
using ProductManagement.Domain.Contracts.Repositories;
using ProductManagement.Domain.Contracts.Services;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Shared.Dtos;
using ProductManagement.Domain.Shared.Responses;

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

        public async Task<CategoryReadDto?> GetAsync(int id)
        {
            var category = await _repository.GetAsync(id);
            return _mapper.Map<CategoryReadDto>(category);
        }

        public async Task<CategoryWriteDto> GetWritableDtoAsync(int id)
        {
            var category = await _repository.GetAsync(id);
            return _mapper.Map<CategoryWriteDto>(category);
        }

        public async Task<IEnumerable<CategoryReadDto>> GetAsync()
        {
            var categories = await _repository.GetAsync();
            return _mapper.Map<IEnumerable<CategoryReadDto>>(categories);
        }

        public async Task<int> CreateAsync(CategoryWriteDto entity)
        {
            var category = _mapper.Map<Category>(entity);
            return await _repository.CreateAsync(category);
        }

        public async Task UpdateAsync(int id, CategoryWriteDto entity)
        {
            var category = _mapper.Map<Category>(entity);
            category.Id = id;

            await _repository.UpdateAsync(category);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
        public async Task<PaginatedResult<CategoryReadDto>> GetPaginatedAsync(int page, int pageSize)
        {
            var paginatedResult = await _repository.GetPaginatedAsync(page, pageSize);
            var mappedData = _mapper.Map<IEnumerable<CategoryReadDto>>(paginatedResult.Items);

            return new PaginatedResult<CategoryReadDto>
            {
                Items = mappedData,
                TotalCount = paginatedResult.TotalCount,
                Page = paginatedResult.Page,
                PageSize = paginatedResult.PageSize
            };
        }

    }
}
