using System.Reflection;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Novel_Nest_DAL;
using UnitTest_Novel_Nest;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using MySqlConnector;
using Interfaces;
using Moq;
using Novel_Nest_Core;

namespace UnitTest_Novel_Nest
{
	[TestClass]
	public class UserServiceTests
	{
		private readonly Mock<IUserRepository> _mockUserRepository;
		private readonly UserService _userService;

		public UserServiceTests()
		{
			_mockUserRepository = new Mock<IUserRepository>();
			_userService = new UserService(_mockUserRepository.Object);
		}

		[TestMethod]
		public async Task GetUserNameAsync_ReturnsUserName()
		{
			// Arrange
			var userId = 1;
			var expectedUserName = "Test User";
			_mockUserRepository.Setup(repo => repo.GetUserNameByIdAsync(userId))
				.ReturnsAsync(expectedUserName);

			// Act
			var result = await _userService.GetUserNameAsync(userId);

			// Assert
			Assert.AreEqual(expectedUserName, result);
			_mockUserRepository.Verify(repo => repo.GetUserNameByIdAsync(userId), Times.Once);
		}
		[TestMethod]
		public async Task GetUserNameAsync_ReturnsStringEmpty_WhenUserNotFound()
		{
			// Arrange
			var userId = 99;
			string expectedUserName = string.Empty; 
			_mockUserRepository.Setup(repo => repo.GetUserNameByIdAsync(userId))
				.ReturnsAsync(expectedUserName);

			// Act
			var result = await _userService.GetUserNameAsync(userId);

			// Assert
			Assert.AreEqual(string.Empty, result);
			_mockUserRepository.Verify(repo => repo.GetUserNameByIdAsync(userId), Times.Once);
		}


		[TestMethod]
		public async Task AuthenticateUserAsync_ReturnsTrue_WhenCredentialsAreValid()
		{
			// Arrange
			var email = "test@example.com";
			var password = "password123";
			var expectedAuthenticationResult = (isAuthenticated: true, Name: "Test User", Id: 1);
			_mockUserRepository.Setup(repo => repo.AuthenticateUserAsync(email, password))
				.ReturnsAsync(expectedAuthenticationResult);

			// Act
			var result = await _userService.AuthenticateUserAsync(email, password);

			// Assert
			Assert.IsTrue(result.isAuthenticated);
			Assert.AreEqual(expectedAuthenticationResult.Name, result.Name);
			Assert.AreEqual(expectedAuthenticationResult.Id, result.Id);
			_mockUserRepository.Verify(repo => repo.AuthenticateUserAsync(email, password), Times.Once);
		}

		[TestMethod]
		public async Task AuthenticateUserAsync_ReturnsFalse_WhenCredentialsAreInvalid()
		{
			// Arrange
			var email = "wrong@example.com";
			var password = "wrongpassword";
			var expectedAuthenticationResult = (isAuthenticated: false, Name: string.Empty, Id: -1);
			_mockUserRepository.Setup(repo => repo.AuthenticateUserAsync(email, password))
				.ReturnsAsync(expectedAuthenticationResult);

			// Act
			var result = await _userService.AuthenticateUserAsync(email, password);

			// Assert
			Assert.IsFalse(result.isAuthenticated);
			Assert.AreEqual(expectedAuthenticationResult.Name, result.Name);
			Assert.AreEqual(expectedAuthenticationResult.Id, result.Id);
			_mockUserRepository.Verify(repo => repo.AuthenticateUserAsync(email, password), Times.Once);
		}


		[TestMethod]
		public async Task CreateUserAsync_ReturnsTrue_WhenUserIsSuccessfullyCreated()
		{
			// Arrange
			var newUser = new UserModelDTO
			{
				Name = "New User",
				Age = 30,
				Email = "newuser@example.com",
				Password = "newpassword123"
			};
			_mockUserRepository.Setup(repo => repo.CreateUserAsync(newUser))
				.ReturnsAsync(true);

			// Act
			var result = await _userService.CreateUserAsync(newUser);

			// Assert
			Assert.IsTrue(result);
			_mockUserRepository.Verify(repo => repo.CreateUserAsync(It.IsAny<UserModelDTO>()), Times.Once);
		}

		[TestMethod]
		public async Task CreateUserAsync_ReturnsFalse_WhenUserCreationFails()
		{
			// Arrange
			var newUser = new UserModelDTO
			{
				Name = "Fail User",
				Age = 25,
				Email = "failuser@example.com",
				Password = "failpassword"
			};
			_mockUserRepository.Setup(repo => repo.CreateUserAsync(newUser))
				.ReturnsAsync(false); // Simuleer een mislukking bij het aanmaken van een gebruiker

			// Act
			var result = await _userService.CreateUserAsync(newUser);

			// Assert
			Assert.IsFalse(result);
			_mockUserRepository.Verify(repo => repo.CreateUserAsync(It.IsAny<UserModelDTO>()), Times.Once);
		}


	}
}