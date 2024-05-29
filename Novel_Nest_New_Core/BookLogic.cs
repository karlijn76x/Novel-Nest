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
            this.bookRepository.AddBookAsync(book);
        }

        public List<CategoryDTO> GetCategories()
        {
            return this.bookRepository.GetCategories();
        }

        public List<BookDTO> GetBooks() 
        {
            return this.bookRepository.GetBooks();
        }

        public async Task<bool> DeleteBookAsync(int Id)
        {
            return await this.bookRepository.DeleteBookAsync(Id);
        }

        public async Task<bool> EditBookAsync(BookDTO book)
        {
            return await this.bookRepository.EditBookAsync(book);
        }
		public async Task<bool> AddBookToNightstandAsync(NightstandBookDTO nightstandBook)
        {
            return await this.bookRepository.AddBookToNightstandAsync(nightstandBook);
        }
		

		public List<NightstandBookDTO> GetNightstandBooks()
		{
			return this.bookRepository.GetNightstandBooks(); 
		}
        public async Task<bool> DeleteNightstandBookAsync(int Id)
        {
            return await this.bookRepository.DeleteNightstandBookAsync(Id);
        }



    }
}
