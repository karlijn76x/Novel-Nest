using Interfaces;
using Models;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;

namespace Novel_Nest_Core
{
    public class BookService
    {
        private readonly IBookRepository _bookRepository;

        public async Task<bool> AddBookFromSearchAsync(OpenLibraryBook book, int userId)
        {
           
            var bookDto = new BookDTO
            {
                Title = book.Title,
                Author = string.Join(", ", book.AuthorName), 
                CategoryId = 1, 
                UserId = userId,
                CoverImageUrl = book.CoverImageUrl
            };

            return await _bookRepository.AddBookAsync(bookDto);
        }

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public async Task<string> GetUserNameAsync(int userId)
        {
            return await _bookRepository.GetUserNameByIdAsync(userId);
        }

        public async Task<bool> AddBookAsync(BookDTO book)
        {
            return await _bookRepository.AddBookAsync(book);
        }

        public List<CategoryDTO> GetCategories()
        {
            return _bookRepository.GetCategories();
        }

        public List<BookDTO> GetBooks(int userId)
        {
            return _bookRepository.GetBooks(userId);
        }
        public async Task<BookDTO> GetBookByUserIdAndBookId(int userId, int bookId)
        {
            return await _bookRepository.GetBookByUserIdAndBookId(userId, bookId);
        }

        public async Task<bool> DeleteBookAsync(int Id)
        {
            return await _bookRepository.DeleteBookAsync(Id);
        }

        public async Task<bool> EditBookAsync(BookDTO book)
        {
            return await _bookRepository.EditBookAsync(book);
        }

		public async Task<bool> AddBookToNightstandAsync(NightstandBookDTO nightstandBook)
		{
			return await _bookRepository.AddBookToNightstandAsync(nightstandBook);
		}

		public List<NightstandBookDTO> GetNightstandBooks(int userId)
		{
			return _bookRepository.GetNightstandBooks(userId);
		}

		public async Task<bool> DeleteNightstandBookAsync(int Id)
		{
			return await _bookRepository.DeleteNightstandBookAsync(Id);
		}
        public async Task<bool> IsBookInNightstandAsync(int bookId)
        {
            return await _bookRepository.IsBookInNightstandAsync(bookId);
        }
        public async Task<bool> AddBookFromApiAsync(BookDTO book)
        {
            return await _bookRepository.AddBookFromApiAsync(book);
        }
        public async Task<NightstandBookDTO> GetNightstandBookByUserIdAndBookId(int userId, int bookId)
        {
			return await _bookRepository.GetNightstandBookByUserIdAndBookId(userId, bookId);
		}
        public async Task<bool> ReviewNightstandBookAsync(NightstandBookDTO nightstandBook)
        {
            return await _bookRepository.ReviewNightstandBookAsync(nightstandBook);
        }


    }
}
