using Interfaces;
using Models;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;

namespace Novel_Nest_Core
{
    public class BookService
    {
        private readonly IBookRepository _bookRepository;


        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<bool> AddBookAsync(BookModel book)
        {
            return await _bookRepository.AddBookAsync(book);
        }

        public List<BookModel> GetBooks(int userId)
        {
            return _bookRepository.GetBooks(userId);
        }

        public async Task<BookModel> GetBookByUserIdAndBookId(int userId, int bookId)
        {
            return await _bookRepository.GetBookByUserIdAndBookId(userId, bookId);
        }

        public async Task<bool> DeleteBookAsync(int Id)
        {
            return await _bookRepository.DeleteBookAsync(Id);
        }

        public async Task<bool> EditLibraryBookAsync(BookModel book)
        {
            return await _bookRepository.EditLibraryBookAsync(book);
        }

		public async Task<bool> AddBookToNightstandAsync(NightstandBookModel nightstandBook)
		{
			return await _bookRepository.AddBookToNightstandAsync(nightstandBook);
		}

		public List<NightstandBookModel> GetNightstandBooks(int userId)
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
     
        public async Task<NightstandBookModel> GetNightstandBookByUserIdAndBookId(int userId, int bookId)
        {
			return await _bookRepository.GetNightstandBookByUserIdAndBookId(userId, bookId);
		}

        public async Task<bool> ReviewNightstandBookAsync(NightstandBookModel nightstandBook)
        {
            return await _bookRepository.ReviewNightstandBookAsync(nightstandBook);
        }
    }
}
