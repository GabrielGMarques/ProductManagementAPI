using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductManagement.Domain.Dtos;
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
            var category = new CategoryDto
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
            var categories = new List<CategoryDto>
            {
                new CategoryDto { Id = 1, Description = "Category1", IsActive = true },
                new CategoryDto { Id = 2, Description = "Category2", IsActive = true },
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
            var categoryToCreate = new CategoryDto
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
            var categoryId = 1;

            var category = new CategoryDto
            {
                Id = categoryId,
                Description = "ExistingCategory",
                IsActive = true
            };

            var updatedCategory = new CategoryDto
            {
                Id = categoryId,
                Description = "UpdatedCategory",
                IsActive = false
            };

            var categoryService = MockServicesBuilder.BuildCategoryService();

            // Act
            var resultId = await categoryService.CreateAsync(category);

            updatedCategory.Id = resultId;
            await categoryService.UpdateAsync(updatedCategory);

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
            var category = new CategoryDto
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
    }
}
