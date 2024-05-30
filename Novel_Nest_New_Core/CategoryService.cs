using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Novel_Nest_DAL;

namespace Novel_Nest_Core
{

	public class CategoryService
	{
		private CategoryRepository categoryRepository;

		public CategoryService()
		{
			categoryRepository = new CategoryRepository(new MyDbContext("Server=127.0.0.1;Database=Novel_Nest_Db;Uid=root;"));
		}

		public void AddCategory(CategoryDTO category)
		{
			categoryRepository.AddCategoryAsync(category);
		}

		public List<CategoryDTO> GetCategories()
		{
			return categoryRepository.GetCategories();
		}

		public async Task<bool> DeleteCategoryAsync(int Id)
		{
			return await categoryRepository.DeleteCategoryAsync(Id);

		}

		public async Task<bool> EditCategoryAsync(CategoryDTO category)
		{
			return await categoryRepository.EditCategoryAsync(category);
		}

	}
}
