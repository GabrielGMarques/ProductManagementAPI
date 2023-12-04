using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductManagement.Domain.Dtos.CRUD;
using ProjectManagement.Test.Builders;

namespace ProjectManagement.Test
{
    [TestClass]
    public class ProductServiceTests
    {

        [TestMethod]
        public async Task GetProductAsync_ShouldReturnProduct()
        {
            // Arrange

            var product = new ProductWriteDto
            {
                Description = "Test Description",
                ProductCode = "TestCode",
                ProductReference = "TestReference",
                Stock = 10,
                Price = 19.99m,
                IsActive = true,
                CategoryId = 1
            };

            var productService = MockServicesBuilder.BuildProductService();

            // Act
            var productId = await productService.CreateAsync(product);

            var result = await productService.GetAsync(productId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(productId, result.Id);
            Assert.AreEqual("Test Description", result.Description);
            Assert.AreEqual("TestCode", result.ProductCode);
            Assert.AreEqual("TestReference", result.ProductReference);
            Assert.AreEqual(10, result.Stock);
            Assert.AreEqual(19.99m, result.Price);
            Assert.IsTrue(result.IsActive);
            Assert.AreEqual(product.CategoryId, result.CategoryId);
        }

        [TestMethod]
        public async Task GetAllProductsAsync_ShouldReturnAllProducts()
        {
            // Arrange
            var products = new List<ProductWriteDto>
        {
            new ProductWriteDto { Description = "Description1", ProductCode = "Code1", ProductReference = "Ref1", Stock = 5, Price = 15.99m, IsActive = true, CategoryId = 1  },
            new ProductWriteDto { Description = "Description2", ProductCode = "Code2", ProductReference = "Ref2", Stock = 10, Price = 29.99m, IsActive = true, CategoryId = 2  },
            // Add more products as needed
        };

            var productService = MockServicesBuilder.BuildProductService();

            foreach (var product in products) await productService.CreateAsync(product);

            // Act
            var result = (await productService.GetAsync()).ToList();

            // Assert
            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(products.Select(p => p.Description).ToList(), result.Select(p => p.Description).ToList());
            CollectionAssert.AreEqual(products.Select(p => p.ProductCode).ToList(), result.Select(p => p.ProductCode).ToList());
            CollectionAssert.AreEqual(products.Select(p => p.ProductReference).ToList(), result.Select(p => p.ProductReference).ToList());
            CollectionAssert.AreEqual(products.Select(p => p.Stock).ToList(), result.Select(p => p.Stock).ToList());
            CollectionAssert.AreEqual(products.Select(p => p.Price).ToList(), result.Select(p => p.Price).ToList());
            CollectionAssert.AreEqual(products.Select(p => p.IsActive).ToList(), result.Select(p => p.IsActive).ToList());
            CollectionAssert.AreEqual(products.Select(p => p.CategoryId).ToList(), result.Select(p => p.CategoryId).ToList());

            for (int i = 0; i < products.Count; i++)
            {
                Assert.AreEqual(products[i].CategoryId, result[i].CategoryId);
            }
        }

        [TestMethod]
        public async Task CreateProductAsync_ShouldReturnGeneratedId()
        {
            // Arrange
            var productToCreate = new ProductWriteDto
            {
                Description = "NewProduct",
                Stock = 5,
                Price = 25.99m,
                IsActive = true,
                CategoryId = 1
            };

            var productService = MockServicesBuilder.BuildProductService();

            // Act
            var resultId = await productService.CreateAsync(productToCreate);
            var result = await productService.GetAsync(resultId);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task UpdateProductAsync_ShouldUpdateProduct()
        {
            var product = new ProductWriteDto
            {
                Description = "ExistingProduct",
                Stock = 10,
                Price = 19.99m,
                IsActive = true,
                CategoryId = 1
            };

            var updatedProduct = new ProductWriteDto
            {
                Description = "UpdatedProduct",
                Stock = 15,
                Price = 29.99m,
                IsActive = false,
                CategoryId = 2
            };

            var productService = MockServicesBuilder.BuildProductService();

            // Act
            var resultId = await productService.CreateAsync(product);
            
            await productService.UpdateAsync(resultId, updatedProduct);

            // Assert
            var productResult = await productService.GetAsync(resultId);

            Assert.IsNotNull(productResult);
            Assert.AreEqual(resultId, productResult.Id);
            Assert.AreEqual(updatedProduct.Description, productResult.Description);
            Assert.AreEqual(updatedProduct.ProductCode, productResult.ProductCode);
            Assert.AreEqual(updatedProduct.ProductReference, productResult.ProductReference);
            Assert.AreEqual(updatedProduct.Stock, productResult.Stock);
            Assert.AreEqual(updatedProduct.Price, productResult.Price);
            Assert.AreEqual(updatedProduct.IsActive, productResult.IsActive);
            Assert.AreEqual(updatedProduct.CategoryId, productResult.CategoryId);
        }

        [TestMethod]
        public async Task DeleteProductAsync_ShouldDeleteProduct()
        {
            var product = new ProductWriteDto
            {
                Description = "ExistingProduct",
                Stock = 10,
                Price = 19.99m,
                IsActive = true,
                CategoryId = 1
            };

            var productService = MockServicesBuilder.BuildProductService();

            // Act
            var productId = await productService.CreateAsync(product);
            await productService.DeleteAsync(productId);

            // Assert
            var result = await productService.GetAsync(productId);
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task GetPaginatedProductsAsync_ShouldReturnPaginatedResult()
        {
            // Arrange
            var page = 1;
            var pageSize = 10;
            var products = Enumerable.Range(1, 20)
                .Select(i => new ProductWriteDto
                {
                    Description = $"Description{i}",
                    Stock = 5,
                    Price = 15.99m,
                    IsActive = true,
                    CategoryId = 1
                })
                .ToList();

            var productService = MockServicesBuilder.BuildProductService();

            foreach (var product in products) await productService.CreateAsync(product);
            // Act
            var result = await productService.GetPaginatedAsync(page, pageSize);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(pageSize, result.Items?.Count());
            Assert.AreEqual(products.Count, result.TotalCount);
            Assert.AreEqual(pageSize, result.PageSize);
        }
    }
}