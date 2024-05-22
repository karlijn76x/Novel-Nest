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

		public List<BookDTO> GetBooks();

		public Task<bool> DeleteBookAsync(int Id);

		public Task<bool> EditBookAsync(BookDTO book);
		public Task<bool> AddBookToNightstandAsync(NightstandBookDTO nightstandBook);

		public List<NightstandBookDTO> GetNightstandBooks();

		public Task<bool> DeleteNightstandBookAsync(int bookId);

	}
}
