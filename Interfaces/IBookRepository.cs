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
		public Task<bool> AddBookAsync(BookDTO book);

		public List<CategoryDTO> GetCategories();

		public List<BookDTO> GetBooks(int userId);
		public Task<BookDTO> GetBookByUserIdAndBookId(int userId, int bookId);

        public Task<bool> DeleteBookAsync(int Id);

		public Task<bool> EditBookAsync(BookDTO book);
		public Task<bool> AddBookToNightstandAsync(NightstandBookDTO nightstandBook);

		public List<NightstandBookDTO> GetNightstandBooks(int userId);

		public Task<bool> DeleteNightstandBookAsync(int bookId);
		public Task<bool> IsBookInNightstandAsync(int bookId);
		public Task<bool> AddBookFromApiAsync(BookDTO book);
		public Task<NightstandBookDTO> GetNightstandBookByUserIdAndBookId(int userId, int bookId);
        public Task<bool> ReviewNightstandBookAsync(NightstandBookDTO nightstandBook);

    }
}
