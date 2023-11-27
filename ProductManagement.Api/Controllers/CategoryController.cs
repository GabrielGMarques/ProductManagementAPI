using Microsoft.AspNetCore.Mvc;
using ProductManagement.Api.Dtos;
using ProductManagement.Domain.Contracts.Services;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Dtos;

namespace ProductManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly ICategoryService _categoryService;

        public CategoryController(
            ILogger<CategoryController> logger,
            ICategoryService categoryService)
        {
            _logger = logger;
            _categoryService = categoryService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CategoryDto), 200)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> GetCategory(int id)
        {
            try
            {
                var category = await _categoryService.GetAsync(id);
                if (category == null)
                    return NotFound();

                return Ok(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting category with ID: {id}.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CategoryDto>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var categories = await _categoryService.GetAsync();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting categories.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), 200)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> CreateCategory(CategoryDto category)
        {
            try
            {
                var resultId = await _categoryService.CreateAsync(category);
                return Ok(resultId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating category.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> UpdateCategory(CategoryDto category)
        {
            try
            {
                await _categoryService.UpdateAsync(category);
                return Ok("Category updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating category with ID: {category.Id}.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                await _categoryService.DeleteAsync(id);
                return Ok("Category deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting category with ID: {id}.");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
