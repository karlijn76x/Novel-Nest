using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;
using Models;
using Novel_Nest_DAL;

namespace Novel_Nest_Core
{
    public class BookLogic
    {
        private BookRepository bookRepository;

        public BookLogic() 
        {
            bookRepository = new BookRepository(new MyDbContext("Server=127.0.0.1;Database=Novel_Nest_Db;Uid=root;"));
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
    }
}
