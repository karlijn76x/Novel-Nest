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
        public Task<List<CategoryModel>> GetAllCategoriesFromUsersAsync();
        public Task<bool> DeleteCategoryFromUserAsync(int categoryId);
        public Task<List<BookModel>> GetAllBooksAsync();
        public Task<bool> EditBookAsync(BookModel book);
        public Task<bool> DeleteBookAsync(int bookId, int userId);
        public Task<bool> AddDefaultCategoryAsync(CategoryModel category);
        public Task<List<CategoryModel>> GetDefaultCategoriesAsync();
        public Task<bool> EditDefaultCategoryAsync(CategoryModel category);
        public Task<bool> DeleteDefaultCategoryAsync(int categoryId);
	}
}
