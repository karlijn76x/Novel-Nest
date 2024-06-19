using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Novel_Nest_Core;
using Interfaces;
using Moq;
using Models;


namespace UnitTest_Novel_Nest
{
	[TestClass]
	public class BookServiceTests
	{
		private readonly Mock<IBookRepository> _mockBookRepository;
		private readonly BookService _bookService;

		public BookServiceTests()
		{
			_mockBookRepository = new Mock<IBookRepository>();
			_bookService = new BookService(_mockBookRepository.Object);
		}

		[TestMethod]
		public void GetBooks_ReturnsBooks()
		{
			// Arrange
			var userId = 1;
			var expectedBooks = new List<BookDTO>
			{
				new BookDTO { Id = 1, Title = "Test Book 1" },
				new BookDTO { Id = 2, Title = "Test Book 2" }
			};
			_mockBookRepository.Setup(repo => repo.GetBooks(userId))
				.Returns(expectedBooks);

			// Act
			var result = _bookService.GetBooks(userId);

			// Assert
			Assert.AreEqual(expectedBooks, result);
			_mockBookRepository.Verify(repo => repo.GetBooks(userId), Times.Once);
		}

		[TestMethod]
		public void GetBooks_WhenNoBooksFound_ReturnsEmptyList()
		{
			// Arrange
			var userId = 99;
			var expectedBooks = new List<BookDTO>();
			_mockBookRepository.Setup(repo => repo.GetBooks(userId))
				.Returns(expectedBooks);

			// Act
			var result = _bookService.GetBooks(userId);

			// Assert
			Assert.AreEqual(expectedBooks, result);
			_mockBookRepository.Verify(repo => repo.GetBooks(userId), Times.Once);
		}

		[TestMethod]
		public async Task GetBookByUserIdAndBookId_ReturnsBook()
		{
			// Arrange
			var userId = 1;
			var bookId = 1;
			var expectedBook = new BookDTO { Id = 1, Title = "Test Book" };
			_mockBookRepository.Setup(repo => repo.GetBookByUserIdAndBookId(userId, bookId))
				.ReturnsAsync(expectedBook);

			// Act
			var result = await _bookService.GetBookByUserIdAndBookId(userId, bookId);

			// Assert
			Assert.AreEqual(expectedBook, result);
			_mockBookRepository.Verify(repo => repo.GetBookByUserIdAndBookId(userId, bookId), Times.Once);
		}

		[TestMethod]
		public void GetBookByUserIdAndBookId_WhenBookNotFound_ReturnsNull()
		{
			// Arrange
			var userId = 1;
			var bookId = 99;
			BookDTO? expectedBook = null;
			_mockBookRepository.Setup(repo => repo.GetBookByUserIdAndBookId(userId, bookId))
				.ReturnsAsync(expectedBook);

			// Act
			var result = _bookService.GetBookByUserIdAndBookId(userId, bookId).Result;

			// Assert
			Assert.IsNull(result);
			_mockBookRepository.Verify(repo => repo.GetBookByUserIdAndBookId(userId, bookId), Times.Once);
		}

		[TestMethod]
		public async Task AddBookAsync_WhenBookAdded_ReturnsTrue()
		{
			// Arrange
			var book = new BookDTO { Title = "Test Book" };
			_mockBookRepository.Setup(repo => repo.AddBookAsync(book))
				.ReturnsAsync(true);

			// Act
			var result = await _bookService.AddBookAsync(book);

			// Assert
			Assert.IsTrue(result);
			_mockBookRepository.Verify(repo => repo.AddBookAsync(book), Times.Once);
		}

		[TestMethod]
		public void AddBookAsync_WhenBookCantBeAdded_ReturnsFalse() {
			// Arrange
			var book = new BookDTO { Title = "Test Book" };
			_mockBookRepository.Setup(repo => repo.AddBookAsync(book))
				.ReturnsAsync(false);

			// Act
			var result = _bookService.AddBookAsync(book).Result;

			// Assert
			Assert.IsFalse(result);
			_mockBookRepository.Verify(repo => repo.AddBookAsync(book), Times.Once);
		}

		[TestMethod]
		public async Task DeleteBookAsync_WhenBookDeleted_ReturnsTrue()
		{
			// Arrange
			var bookId = 1;
			_mockBookRepository.Setup(repo => repo.DeleteBookAsync(bookId))
				.ReturnsAsync(true);

			// Act
			var result = await _bookService.DeleteBookAsync(bookId);

			// Assert
			Assert.IsTrue(result);
			_mockBookRepository.Verify(repo => repo.DeleteBookAsync(bookId), Times.Once);
		}

		[TestMethod]
		public void DeleteBookAsync_WhenBookCantBeDeleted_ReturnsFalse()
		{
			// Arrange
			var bookId = 1;
			_mockBookRepository.Setup(repo => repo.DeleteBookAsync(bookId))
				.ReturnsAsync(false);

			// Act
			var result = _bookService.DeleteBookAsync(bookId).Result;

			// Assert
			Assert.IsFalse(result);
			_mockBookRepository.Verify(repo => repo.DeleteBookAsync(bookId), Times.Once);
		}

		[TestMethod]
		public async Task EditLibraryBookAsync_WhenBookEdited_ReturnsTrue()
		{
			// Arrange
			var book = new BookDTO { Id = 1, Title = "Test Book" };
			_mockBookRepository.Setup(repo => repo.EditLibraryBookAsync(book))
				.ReturnsAsync(true);

			// Act
			var result = await _bookService.EditLibraryBookAsync(book);

			// Assert
			Assert.IsTrue(result);
			_mockBookRepository.Verify(repo => repo.EditLibraryBookAsync(book), Times.Once);
		}

		[TestMethod]
		public void EditLibraryBookAsync_WhenBookCantBeEdited_ReturnsFalse()
		{
			// Arrange
			var book = new BookDTO { Id = 1, Title = "Test Book" };
			_mockBookRepository.Setup(repo => repo.EditLibraryBookAsync(book))
				.ReturnsAsync(false);

			// Act
			var result = _bookService.EditLibraryBookAsync(book).Result;

			// Assert
			Assert.IsFalse(result);
			_mockBookRepository.Verify(repo => repo.EditLibraryBookAsync(book), Times.Once);
		}

		[TestMethod]
		public async Task AddBookToNightstandAsync_WhenBookAdded_ReturnsTrue()
		{
			// Arrange
			var nightstandBook = new NightstandBookDTO { BookId = 1, UserId = 1 };
			_mockBookRepository.Setup(repo => repo.AddBookToNightstandAsync(nightstandBook))
				.ReturnsAsync(true);

			// Act
			var result = await _bookService.AddBookToNightstandAsync(nightstandBook);

			// Assert
			Assert.IsTrue(result);
			_mockBookRepository.Verify(repo => repo.AddBookToNightstandAsync(nightstandBook), Times.Once);
		}

		[TestMethod]
		public void AddBookToNightstandAsync_WhenBookCantBeAdded_ReturnsFalse()
		{
			// Arrange
			var nightstandBook = new NightstandBookDTO { BookId = 1, UserId = 1 };
			_mockBookRepository.Setup(repo => repo.AddBookToNightstandAsync(nightstandBook))
				.ReturnsAsync(false);

			// Act
			var result = _bookService.AddBookToNightstandAsync(nightstandBook).Result;

			// Assert
			Assert.IsFalse(result);
			_mockBookRepository.Verify(repo => repo.AddBookToNightstandAsync(nightstandBook), Times.Once);
		}

		[TestMethod]
		public void GetNightstandBooks_ReturnsNightstandBooks()
		{
			// Arrange
			var userId = 1;
			var expectedNightstandBooks = new List<NightstandBookDTO>
			{
				new NightstandBookDTO { Id = 1, BookId = 1 },
				new NightstandBookDTO { Id = 2, BookId = 2 }
			};
			_mockBookRepository.Setup(repo => repo.GetNightstandBooks(userId))
				.Returns(expectedNightstandBooks);

			// Act
			var result = _bookService.GetNightstandBooks(userId);

			// Assert
			Assert.AreEqual(expectedNightstandBooks, result);
			_mockBookRepository.Verify(repo => repo.GetNightstandBooks(userId), Times.Once);
		}

		[TestMethod]
		public void GetNightstandBooks_WhenNoNightstandBooksFound_ReturnsEmptyList()
		{
			// Arrange
			var userId = 99;
			var expectedNightstandBooks = new List<NightstandBookDTO>();
			_mockBookRepository.Setup(repo => repo.GetNightstandBooks(userId))
				.Returns(expectedNightstandBooks);

			// Act
			var result = _bookService.GetNightstandBooks(userId);

			// Assert
			Assert.AreEqual(expectedNightstandBooks, result);
			_mockBookRepository.Verify(repo => repo.GetNightstandBooks(userId), Times.Once);
		}

		[TestMethod]
		public async Task DeleteNightstandBookAsync_WhenNightstandBookDeleted_ReturnsTrue()
		{
			// Arrange
			var nightstandBookId = 1;
			_mockBookRepository.Setup(repo => repo.DeleteNightstandBookAsync(nightstandBookId))
				.ReturnsAsync(true);

			// Act
			var result = await _bookService.DeleteNightstandBookAsync(nightstandBookId);

			// Assert
			Assert.IsTrue(result);
			_mockBookRepository.Verify(repo => repo.DeleteNightstandBookAsync(nightstandBookId), Times.Once);
		}

		[TestMethod]
		public void DeleteNightstandBookAsync_WhenNightstandBookCantBeDeleted_ReturnsFalse()
		{
			// Arrange
			var nightstandBookId = 1;
			_mockBookRepository.Setup(repo => repo.DeleteNightstandBookAsync(nightstandBookId))
				.ReturnsAsync(false);

			// Act
			var result = _bookService.DeleteNightstandBookAsync(nightstandBookId).Result;

			// Assert
			Assert.IsFalse(result);
			_mockBookRepository.Verify(repo => repo.DeleteNightstandBookAsync(nightstandBookId), Times.Once);
		}

		[TestMethod]
		public async Task IsBookInNightstandAsync_WhenBookIsInNightstand_ReturnsTrue()
		{
			// Arrange
			var bookId = 1;
			_mockBookRepository.Setup(repo => repo.IsBookInNightstandAsync(bookId))
				.ReturnsAsync(true);

			// Act
			var result = await _bookService.IsBookInNightstandAsync(bookId);

			// Assert
			Assert.IsTrue(result);
			_mockBookRepository.Verify(repo => repo.IsBookInNightstandAsync(bookId), Times.Once);
		}

		[TestMethod]
		public async Task IsBookInNightstandAsync_WhenBookIsNotInNightstand_ReturnsFalse()
		{
			// Arrange
			var bookId = 1;
			_mockBookRepository.Setup(repo => repo.IsBookInNightstandAsync(bookId))
				.ReturnsAsync(false);

			// Act
			var result = await _bookService.IsBookInNightstandAsync(bookId);

			// Assert
			Assert.IsFalse(result);
			_mockBookRepository.Verify(repo => repo.IsBookInNightstandAsync(bookId), Times.Once);
		}

		[TestMethod]
		public async Task GetNightstandBookByUserIdAndBookId_ReturnsNightstandBook()
		{
			// Arrange
			var userId = 1;
			var bookId = 1;
			var expectedNightstandBook = new NightstandBookDTO { Id = 1, BookId = 1, UserId = 1 };
			_mockBookRepository.Setup(repo => repo.GetNightstandBookByUserIdAndBookId(userId, bookId))
				.ReturnsAsync(expectedNightstandBook);

			// Act
			var result = await _bookService.GetNightstandBookByUserIdAndBookId(userId, bookId);

			// Assert
			Assert.AreEqual(expectedNightstandBook, result);
			_mockBookRepository.Verify(repo => repo.GetNightstandBookByUserIdAndBookId(userId, bookId), Times.Once);
		}

		[TestMethod]
		public async Task GetNightstandBookByUserIdAndBookId_WhenNightstandBookNotFound_ReturnsNull()
		{
			// Arrange
			var userId = 1;
			var bookId = 99;
			NightstandBookDTO? expectedNightstandBook = null;
			_mockBookRepository.Setup(repo => repo.GetNightstandBookByUserIdAndBookId(userId, bookId))
				.ReturnsAsync(expectedNightstandBook);

			// Act
			var result = await _bookService.GetNightstandBookByUserIdAndBookId(userId, bookId);

			// Assert
			Assert.IsNull(result);
			_mockBookRepository.Verify(repo => repo.GetNightstandBookByUserIdAndBookId(userId, bookId), Times.Once);
		}

		[TestMethod]
		public async Task ReviewNightstandBookAsync_WhenNightstandBookReviewed_ReturnsTrue()
		{
			// Arrange
			var nightstandBook = new NightstandBookDTO { Id = 1, BookId = 1, UserId = 1 };
			_mockBookRepository.Setup(repo => repo.ReviewNightstandBookAsync(nightstandBook))
				.ReturnsAsync(true);

			// Act
			var result = await _bookService.ReviewNightstandBookAsync(nightstandBook);

			// Assert
			Assert.IsTrue(result);
			_mockBookRepository.Verify(repo => repo.ReviewNightstandBookAsync(nightstandBook), Times.Once);
		}

		[TestMethod]
		public void ReviewNightstandBookAsync_WhenNightstandBookCantBeReviewed_ReturnsFalse()
		{
			// Arrange
			var nightstandBook = new NightstandBookDTO { Id = 1, BookId = 1, UserId = 1 };
			_mockBookRepository.Setup(repo => repo.ReviewNightstandBookAsync(nightstandBook))
				.ReturnsAsync(false);

			// Act
			var result = _bookService.ReviewNightstandBookAsync(nightstandBook).Result;

			// Assert
			Assert.IsFalse(result);
			_mockBookRepository.Verify(repo => repo.ReviewNightstandBookAsync(nightstandBook), Times.Once);
		}

	}
}
