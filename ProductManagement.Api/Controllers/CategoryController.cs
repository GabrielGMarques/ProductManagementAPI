using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Domain.Contracts.Services;
using ProductManagement.Domain.Dtos.CRUD;
using ProductManagement.Domain.Dtos.Responses;

namespace ProductManagement.Api.Controllers
{
    [Authorize]
    [Authorize(Roles = "Admin")] //TODO use the UserRole enum
    [ApiController]
    [Route("api/categories")]
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
        [ProducesResponseType(typeof(CategoryReadDto), 200)]
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
        [ProducesResponseType(typeof(PaginatedResult<CategoryReadDto>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> GetProductsPaginated(int page = 1, int pageSize = 10)
        {
            try
            {
                var paginatedResult = await _categoryService.GetPaginatedAsync(page, pageSize);
                return Ok(paginatedResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting paginated products.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), 200)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> CreateCategory(CategoryWriteDto category)
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

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> UpdateCategory([FromRoute] int id, [FromBody] CategoryWriteDto category)
        {
            try
            {
                await _categoryService.UpdateAsync(id, category);
                return Ok("Category updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating category with ID: {id}.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> UpdateCategoryPartially(
            [FromRoute] int id,
            [FromBody] JsonPatchDocument<CategoryWriteDto> categoryDoc)
        {
            try
            {
                var categoryDto = await _categoryService.GetWritableDtoAsync(id);
                categoryDoc.ApplyTo(categoryDto, ModelState);

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                await _categoryService.UpdateAsync(id, categoryDto);

                return Ok("Category updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating category with ID: {id}.");
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
