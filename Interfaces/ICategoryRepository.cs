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
        List<CategoryDTO> GetCategories();
        Task<bool> DeleteCategoryAsync(int Id);
        Task<bool> EditCategoryAsync(CategoryDTO category);
		public Task<bool> IsCategoryInUseAsync(int categoryId);

	}
}
