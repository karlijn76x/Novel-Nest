using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Interfaces
{
	public interface IBookRepository
	{
        public Task<bool> AddBookAsync(BookModel book);
		public List<BookModel> GetBooks(int userId);
		public Task<BookModel> GetBookByUserIdAndBookId(int userId, int bookId);
        public Task<bool> DeleteBookAsync(int Id);
		public Task<bool> EditLibraryBookAsync(BookModel book);
		public Task<bool> AddBookToNightstandAsync(NightstandBookModel nightstandBook);
		public List<NightstandBookModel> GetNightstandBooks(int userId);
		public Task<bool> DeleteNightstandBookAsync(int bookId);
		public Task<bool> IsBookInNightstandAsync(int bookId);
		public Task<NightstandBookModel> GetNightstandBookByUserIdAndBookId(int userId, int bookId);
        public Task<bool> ReviewNightstandBookAsync(NightstandBookModel nightstandBook);

    }
}
