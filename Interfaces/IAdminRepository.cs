using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Interfaces
{
    public interface IAdminRepository
    {
        public Task<List<CategoryDTO>> GetAllCategoriesFromUsersAsync();
        public Task<bool> DeleteCategoryFromUserAsync(int categoryId);
        public Task<List<BookDTO>> GetAllBooksAsync();
        public Task<bool> EditBookAsync(BookDTO book);
        public Task<bool> DeleteBookAsync(int bookId, int userId);
        public Task<bool> AddDefaultCategoryAsync(CategoryDTO category);
        public Task<List<CategoryDTO>> GetDefaultCategoriesAsync();
        public Task<bool> EditDefaultCategoryAsync(CategoryDTO category);
        public Task<bool> DeleteDefaultCategoryAsync(int categoryId);

	}
}
