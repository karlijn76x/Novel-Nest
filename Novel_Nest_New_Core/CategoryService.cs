using Interfaces;
using Models;

namespace Novel_Nest_Core
{
    public class CategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<bool> AddCategoryAsync(CategoryDTO category)
        {
            return await _categoryRepository.AddCategoryAsync(category);
        }

        public List<CategoryDTO> GetCategories()
        {
            return _categoryRepository.GetCategories();
        }

        public async Task<bool> DeleteCategoryAsync(int Id)
        {
            return await _categoryRepository.DeleteCategoryAsync(Id);
        }

        public async Task<bool> EditCategoryAsync(CategoryDTO category)
        {
            return await _categoryRepository.EditCategoryAsync(category);
        }
    }
}
