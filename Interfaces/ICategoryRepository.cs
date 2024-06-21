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
        Task<bool> AddCategoryAsync(CategoryDTO category);
        List<CategoryDTO> GetCategories(int userId);
		public Task<List<CategoryDTO>> GetUserAndDefaultCategoriesAsync(int userId);
		public Task<bool> DeleteCategoryAsync(int Id, int userId);
        public Task<bool> EditCategoryAsync(CategoryDTO category);
		public Task<bool> IsCategoryInUseAsync(int categoryId);

	}
}
