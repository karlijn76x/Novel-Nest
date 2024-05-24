using Interfaces;
using Models;
using Novel_Nest_DAL;

namespace Novel_Nest_Core
{
    public class BookLogic
    {
        private IBookRepository bookRepository;

        public BookLogic(IBookRepository bookRepository) 
        {
            this.bookRepository = bookRepository;
        }

        public void AddBookAsync(BookDTO book)
        {
            bookRepository.AddBookAsync(book);
        }

        public List<CategoryDTO> GetCategories()
        {
            return bookRepository.GetCategories();
        }

        public List<BookDTO> GetBooks() 
        {
            return bookRepository.GetBooks();
        }

        public async Task<bool> DeleteBookAsync(int Id)
        {
            return await bookRepository.DeleteBookAsync(Id);
        }

        public async Task<bool> EditBookAsync(BookDTO book)
        {
            return await bookRepository.EditBookAsync(book);
        }
		public async Task<bool> AddBookToNightstandAsync(NightstandBookDTO nightstandBook)
        {
            return await bookRepository.AddBookToNightstandAsync(nightstandBook);
        }
		

		public List<NightstandBookDTO> GetNightstandBooks()
		{
			return bookRepository.GetNightstandBooks(); 
		}
        public async Task<bool> DeleteNightstandBookAsync(int Id)
        {
            return await bookRepository.DeleteNightstandBookAsync(Id);
        }



    }
}
