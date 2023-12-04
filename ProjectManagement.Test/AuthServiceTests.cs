using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductManagement.Domain.Enums;
using ProductManagement.Domain.Shared.Dtos;
using ProjectManagement.Test.Builders;

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

            var userDto = new LoginDto
            {
                Username = "TestUser",
                Password = "TestPassword"
            };

            var registerDto = new RegisterDto
            {
                Username = userDto.Username,
                Password = userDto.Password,
                Role = UserRole.User
            };


            var serviceResponse = await authService.Register(registerDto);

            var userId = serviceResponse.Data ?? 0;
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

            var userDto = new LoginDto
            {
                Username = "TestUser",
                Password = "TestPassword"
            };

            var registerDto = new RegisterDto
            {
                Username = userDto.Username,
                Password = userDto.Password,
                Role = UserRole.User
            };

            await authService.Register(registerDto);
            
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

            var userDto = new LoginDto
            {
                Username = "TestUser",
                Password = "TestPassword"
            };

            var registerDto = new RegisterDto
            {
                Username = userDto.Username,
                Password = userDto.Password,
                Role = UserRole.User
            };

            await authService.Register(registerDto);

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

            var userDto = new LoginDto
            {
                Username = "NewUser",
                Password = "NewPassword"
            };

            var registerDto = new RegisterDto
            {
                Username = userDto.Username,
                Password = userDto.Password,
                Role = UserRole.User
            };

            // Act
            var result = await authService.Register(registerDto);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Data);
        }

        [TestMethod]
        public async Task Register_WithExistingUsername_ShouldReturnInvalidResponse()
        {
            // Arrange
            var authService = MockServicesBuilder.BuildAuthService();

            var registerDto = new RegisterDto
            {
                Username = "NewUser",
                Password = "NewPassword",
                Role = UserRole.User
            };


            // Act
            await authService.Register(registerDto);
            var result = await authService.Register(registerDto);

            // Assert
            Assert.IsFalse(result.Success);
            Assert.IsNull(result.Data);
            Assert.AreEqual(result.Message, "Username already exists");
        }

        // Add more test methods for different scenarios as needed
    }
}
