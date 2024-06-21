using Interfaces;
using Models;
using Novel_Nest_DAL;

namespace Novel_Nest_Core
{
    public class CategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<bool> AddCategoryAsync(CategoryModel category)
        {
            return await _categoryRepository.AddCategoryAsync(category);
        }

        public List<CategoryModel> GetCategories(int userId)
        {
            return _categoryRepository.GetCategories(userId);
        }

		public async Task<List<CategoryModel>> GetUserAndDefaultCategoriesAsync(int userId)
		{
			return await _categoryRepository.GetUserAndDefaultCategoriesAsync(userId);
		}

		public async Task<bool> DeleteCategoryAsync(int Id, int UserId)
        {
            return await _categoryRepository.DeleteCategoryAsync(Id, UserId);
        }

        public async Task<bool> EditCategoryAsync(CategoryModel category)
        {
            return await _categoryRepository.EditCategoryAsync(category);
        }

		public async Task<bool> IsCategoryInUseAsync(int categoryId)
		{
			return await _categoryRepository.IsCategoryInUseAsync(categoryId);
		}
	}
}
