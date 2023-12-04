using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductManagement.Domain.Shared.Dtos;
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
                Height = 10.4m,
                Width = 10.4m,
                Weight = 15.99m,
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
            Assert.AreEqual(product.Description, result.Description);
            Assert.AreEqual(product.ProductCode, result.ProductCode);
            Assert.AreEqual(product.ProductReference, result.ProductReference);
            Assert.AreEqual(product.Stock, result.Stock);
            Assert.AreEqual(product.Price, result.Price);
            Assert.AreEqual(product.Height, result.Height);
            Assert.AreEqual(product.Width, result.Width);
            Assert.AreEqual(product.Weight, result.Weight);
            Assert.IsTrue(result.IsActive);
            Assert.AreEqual(product.CategoryId, result.CategoryId);
        }

        [TestMethod]
        public async Task GetAllProductsAsync_ShouldReturnAllProducts()
        {
            // Arrange
            var products = new List<ProductWriteDto>
        {
            new ProductWriteDto { Description = "Description1", ProductCode = "Code1", ProductReference = "Ref1", Stock = 5, Price = 15.99m,  Height = 10.4m, Width = 10.4m, Weight = 15.99m, IsActive = true, CategoryId = 1  },
            new ProductWriteDto { Description = "Description2", ProductCode = "Code2", ProductReference = "Ref2", Stock = 10, Price = 29.99m, Height = 10.4m, Width = 10.4m, Weight = 15.99m, IsActive = true, CategoryId = 2  },
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
            CollectionAssert.AreEqual(products.Select(p => p.Height).ToList(), result.Select(p => p.Height).ToList());
            CollectionAssert.AreEqual(products.Select(p => p.Width).ToList(), result.Select(p => p.Width).ToList());
            CollectionAssert.AreEqual(products.Select(p => p.Weight).ToList(), result.Select(p => p.Weight).ToList());
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
                Height = 10.4m,
                Width = 10.4m,
                Weight = 15.99m,
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
            Assert.AreEqual(updatedProduct.Height, productResult.Height);
            Assert.AreEqual(updatedProduct.Width, productResult.Width);
            Assert.AreEqual(updatedProduct.Weight, productResult.Weight);
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