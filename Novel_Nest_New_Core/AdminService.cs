
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

        public async Task<List<CategoryDTO>> GetAllCategoriesAsync()
        {
            return await _adminRepository.GetAllCategoriesFromUsersAsync();
        }

        public async Task<bool> DeleteCategoryAsync(int categoryId)
        {
            return await _adminRepository.DeleteCategoryFromUserAsync(categoryId);
        }

        public async Task<List<BookDTO>> GetAllBooksAsync()
        {
            return await _adminRepository.GetAllBooksAsync();
        }

		public async Task<bool> EditBookAsync(BookDTO book)
		{
			return await _adminRepository.EditBookAsync(book);
		}

		public async Task<bool> DeleteBookAsync(int bookId, int userId)
		{
			return await _adminRepository.DeleteBookAsync(bookId, userId);
		}


	}
}
