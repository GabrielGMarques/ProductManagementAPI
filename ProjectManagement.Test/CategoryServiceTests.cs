using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductManagement.Domain.Shared.Dtos;
using ProjectManagement.Test.Builders;

namespace ProjectManagement.Test
{
    [TestClass]
    public class CategoryServiceTests
    {
        [TestMethod]
        public async Task GetCategoryAsync_ShouldReturnCategory()
        {
            // Arrange
            var category = new CategoryWriteDto
            {
                Description = "TestCategory",
                IsActive = true
            };

            var categoryService = MockServicesBuilder.BuildCategoryService();

            // Act
            var categoryId = await categoryService.CreateAsync(category);
            var result = await categoryService.GetAsync(categoryId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(categoryId, result.Id);
            Assert.AreEqual("TestCategory", result.Description);
            Assert.IsTrue(result.IsActive);
        }

        [TestMethod]
        public async Task GetAllCategoriesAsync_ShouldReturnAllCategories()
        {
            // Arrange
            var categories = new List<CategoryWriteDto>
            {
                new CategoryWriteDto { Description = "Category1", IsActive = true },
                new CategoryWriteDto { Description = "Category2", IsActive = true },
                // Add more categories as needed
            };

            var categoryService = MockServicesBuilder.BuildCategoryService();

            foreach (var category in categories) await categoryService.CreateAsync(category);

            // Act
            var result = (await categoryService.GetAsync()).ToList();

            // Assert
            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(categories.Select(c => c.Description).ToList(), result.Select(c => c.Description).ToList());
            CollectionAssert.AreEqual(categories.Select(c => c.IsActive).ToList(), result.Select(c => c.IsActive).ToList());
        }

        [TestMethod]
        public async Task CreateCategoryAsync_ShouldReturnGeneratedId()
        {
            // Arrange
            var categoryToCreate = new CategoryWriteDto
            {
                Description = "NewCategory",
                IsActive = true
            };

            var categoryService = MockServicesBuilder.BuildCategoryService();

            // Act
            var resultId = await categoryService.CreateAsync(categoryToCreate);
            var result = await categoryService.GetAsync(resultId);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task UpdateCategoryAsync_ShouldUpdateCategory()
        {
            // Arrange
            var category = new CategoryWriteDto
            {
                Description = "ExistingCategory",
                IsActive = true
            };

            var updatedCategory = new CategoryWriteDto
            {
                Description = "UpdatedCategory",
                IsActive = false
            };

            var categoryService = MockServicesBuilder.BuildCategoryService();

            // Act
            var resultId = await categoryService.CreateAsync(category);

            await categoryService.UpdateAsync(resultId, updatedCategory);

            // Assert
            var categoryResult = await categoryService.GetAsync(resultId);

            Assert.IsNotNull(categoryResult);
            Assert.AreEqual(resultId, categoryResult.Id);
            Assert.AreEqual(updatedCategory.Description, categoryResult.Description);
            Assert.AreEqual(updatedCategory.IsActive, categoryResult.IsActive);
        }

        [TestMethod]
        public async Task DeleteCategoryAsync_ShouldDeleteCategory()
        {
            // Arrange
            var category = new CategoryWriteDto
            {
                Description = "ExistingCategory",
                IsActive = true
            };

            var categoryService = MockServicesBuilder.BuildCategoryService();

            // Act
            var categoryId = await categoryService.CreateAsync(category);
            await categoryService.DeleteAsync(categoryId);

            // Assert
            var result = await categoryService.GetAsync(categoryId);
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task GetPaginatedCategoriesAsync_ShouldReturnPaginatedResult()
        {
            // Arrange
            var page = 1;
            var pageSize = 10;
            var categories = Enumerable.Range(1, 20)
                .Select(i => new CategoryWriteDto
                {
                    Description = $"Description{i}",
                    IsActive = true
                })
                .ToList();

            var categoryService = MockServicesBuilder.BuildCategoryService();

            foreach (var category in categories) await categoryService.CreateAsync(category);
            // Act
            var result = await categoryService.GetPaginatedAsync(page, pageSize);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(pageSize, result.Items?.Count());
            Assert.AreEqual(categories.Count, result.TotalCount);
            Assert.AreEqual(pageSize, result.PageSize);
        }
    }
}
