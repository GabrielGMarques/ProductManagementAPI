using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductManagement.Application.Services;
using ProductManagement.Domain.Contracts.Repository;
using ProductManagement.Domain.Dtos;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Enums;
using ProductManagement.Domain.Responses;
using ProjectManagement.Test.Builders;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Test
{
    [TestClass]
    public class AuthServiceTests
    {
        [TestMethod]
        public async Task GetAsync_ShouldReturnUserDto()
        {
            // Arrange
            var authService = MockServicesBuilder.BuildAuthService();

            var userDto = new UserDto
            {
                Username = "TestUser",
                Password = "TestPassword",
                Role = UserRole.User
            };

            var serviceResponse = await authService.Register(userDto);

            var userId = serviceResponse.Data.Value;
            // Act
            var result = await authService.GetAsync(userId);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(userId, result.Data.Id);
            Assert.AreEqual(UserRole.User, result.Data.Role);
        }

        [TestMethod]
        public async Task Login_WithValidCredentials_ShouldReturnJwtToken()
        {
            // Arrange
            var authService = MockServicesBuilder.BuildAuthService();

            var userDto = new UserDto
            {
                Username = "TestUser",
                Password = "TestPassword",
                Role = UserRole.User
            };

            await authService.Register(userDto);
            
            // Act
            var result = await authService.Login(userDto);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Data);
            Assert.IsInstanceOfType(result.Data, typeof(string));
        }

        [TestMethod]
        public async Task Login_WithInValidCredentials_ShouldReturnInvalidResponse()
        {
            // Arrange
            var authService = MockServicesBuilder.BuildAuthService();

            var userDto = new UserDto
            {
                Username = "TestUser",
                Password = "TestPassword",
                Role = UserRole.User
            };

            await authService.Register(userDto);

            userDto.Password = "WrongPassword";
            // Act
            var result = await authService.Login(userDto);

            // Assert
            Assert.IsFalse(result.Success);
            Assert.IsNull(result.Data);
            Assert.AreEqual(result.Message, "Invalid username or password");
        }

        [TestMethod]
        public async Task Register_WithValidUserDto_ShouldReturnGeneratedId()
        {
            // Arrange
            var authService = MockServicesBuilder.BuildAuthService();

            var userDto = new UserDto
            {
                Username = "NewUser",
                Password = "NewPassword",
                Role = UserRole.User
            };

            // Act
            var result = await authService.Register(userDto);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Data);
        }

        [TestMethod]
        public async Task Register_WithExistingUsername_ShouldReturnInvalidResponse()
        {
            // Arrange
            var authService = MockServicesBuilder.BuildAuthService();

            var userDto = new UserDto
            {
                Username = "NewUser",
                Password = "NewPassword",
                Role = UserRole.User
            };

            // Act
            await authService.Register(userDto);
            var result = await authService.Register(userDto);

            // Assert
            Assert.IsFalse(result.Success);
            Assert.IsNull(result.Data);
            Assert.AreEqual(result.Message, "Username already exists");
        }

        // Add more test methods for different scenarios as needed
    }
}
