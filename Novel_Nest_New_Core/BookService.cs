using Interfaces;
using Models;

namespace Novel_Nest_Core
{
    public class BookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<bool> AddBookAsync(BookDTO book)
        {
            return await _bookRepository.AddBookAsync(book);
        }

        public List<CategoryDTO> GetCategories()
        {
            return _bookRepository.GetCategories();
        }

        public List<BookDTO> GetBooks()
        {
            return _bookRepository.GetBooks();
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

        public List<NightstandBookDTO> GetNightstandBooks()
        {
            return _bookRepository.GetNightstandBooks();
        }

        public async Task<bool> DeleteNightstandBookAsync(int Id)
        {
            return await _bookRepository.DeleteNightstandBookAsync(Id);
        }
    }
}
