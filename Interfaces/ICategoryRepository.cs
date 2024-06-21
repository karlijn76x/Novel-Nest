using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Interfaces
{
	public interface ICategoryRepository
	{
        Task<bool> AddCategoryAsync(CategoryModel category);
        List<CategoryModel> GetCategories(int userId);
		public Task<List<CategoryModel>> GetUserAndDefaultCategoriesAsync(int userId);
		public Task<bool> DeleteCategoryAsync(int Id, int userId);
        public Task<bool> EditCategoryAsync(CategoryModel category);
		public Task<bool> IsCategoryInUseAsync(int categoryId);

	}
}
