﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Api.Common;
using ProductManagement.Domain.Contracts.Services;
using ProductManagement.Domain.Shared.Dtos;
using ProductManagement.Domain.Shared.Responses;

namespace ProductManagement.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(
            ILogger<ProductController> logger,
            IProductService productService,
            ICategoryService categoryService)
        {
            _logger = logger;
            _productService = productService;
            _categoryService = categoryService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductReadDto), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> GetProduct(int id)
        {
            try
            {
                var product = await _productService.GetAsync(id);
                if (product == null)
                    return NotFound();

                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting product with ID: {id}.");
                return StatusCode(500, ErrorMessages.UnhandledException);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(PaginatedResult<ProductReadDto>), 200)]
        public async Task<IActionResult> GetPaginated(int page = 1, int pageSize = 10)
        {
            try
            {
                var paginatedResult = await _productService.GetPaginatedAsync(page, pageSize);
                return Ok(paginatedResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting paginated products.");
                return StatusCode(500, ErrorMessages.UnhandledException);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> CreateProduct(ProductWriteDto product)
        {
            try
            {
                var category = await _categoryService.GetAsync(product.CategoryId);

                if (category == null)
                    return BadRequest("The product should have a valid category");

                var productId = await _productService.CreateAsync(product);
                return Ok(productId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating product.");
                return StatusCode(500, ErrorMessages.UnhandledException);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> UpdateProduct([FromRoute] int id, [FromBody] ProductWriteDto product)
        {
            try
            {
                var category = await _categoryService.GetAsync(product.CategoryId);

                if (category == null)
                    return BadRequest("The product should have a valid category");

                await _productService.UpdateAsync(id, product);
                return Ok("Product updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating product with ID: {id}.");
                return StatusCode(500, ErrorMessages.UnhandledException);
            }
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> UpdateProductPartially(
            [FromRoute] int id,
            [FromBody] JsonPatchDocument<ProductWriteDto> productDoc)
        {
            try
            {
                var productDto = await _productService.GetWritableDtoAsync(id);
                productDoc.ApplyTo(productDto, ModelState);

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
               
                var category = await _categoryService.GetAsync(productDto.CategoryId);

                if (category == null)
                    return BadRequest("The product should have a valid category");

                await _productService.UpdateAsync(id, productDto);

                return Ok("Product updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating product with ID: {id}.");
                return StatusCode(500, ErrorMessages.UnhandledException);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                await _productService.DeleteAsync(id);
                return Ok("Product deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting product with ID: {id}.");
                return StatusCode(500, ErrorMessages.UnhandledException);
            }
        }
    }
}
