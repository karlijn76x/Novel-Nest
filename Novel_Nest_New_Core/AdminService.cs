﻿
using Models;
using Interfaces;

namespace Novel_Nest_Core
{
    public class AdminService
    {
        private readonly IAdminRepository _adminRepository;

        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public async Task<List<CategoryModel>> GetAllCategoriesAsync()
        {
            return await _adminRepository.GetAllCategoriesFromUsersAsync();
        }

        public async Task<bool> DeleteCategoryAsync(int categoryId)
        {
            return await _adminRepository.DeleteCategoryFromUserAsync(categoryId);
        }

        public async Task<List<BookModel>> GetAllBooksAsync()
        {
            return await _adminRepository.GetAllBooksAsync();
        }

		public async Task<bool> EditBookAsync(BookModel book)
		{
			return await _adminRepository.EditBookAsync(book);
		}

		public async Task<bool> DeleteBookAsync(int bookId, int userId)
		{
			return await _adminRepository.DeleteBookAsync(bookId, userId);
		}

        public async Task<bool> AddDefaultCategoryAsync(CategoryModel category)
        {
            return await _adminRepository.AddDefaultCategoryAsync(category);
        }

		public async Task<List<CategoryModel>> GetDefaultCategoriesAsync()
		{
			return await _adminRepository.GetDefaultCategoriesAsync();
		}

        public async Task<bool> EditDefaultCategoryAsync(CategoryModel category)
		{
			return await _adminRepository.EditDefaultCategoryAsync(category);
		}
        public async Task<bool> DeleteDefaultCategoryAsync(int categoryId)
		{
			return await _adminRepository.DeleteDefaultCategoryAsync(categoryId);
		}
        
	}
}
