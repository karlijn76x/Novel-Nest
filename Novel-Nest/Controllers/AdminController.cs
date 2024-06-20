using Microsoft.AspNetCore.Mvc;
using Models;
using Novel_Nest.Models;
using Novel_Nest_Core;
using System.Threading.Tasks;

namespace Novel_Nest.Controllers
{
    public class AdminController : Controller
    {
        private readonly AdminService _adminService;

        public AdminController(AdminService adminService)
        {
            _adminService = adminService;
        }

        public IActionResult AdminIndex()
        {
            return View();
        }

        public async Task<IActionResult> ManageCategories()
        {
            var categories = await _adminService.GetAllCategoriesAsync();
            var model = new CategoryUserViewModel
            {
                Category = categories
               
            };
            return View(model);
        }

        public async Task<IActionResult> ManageBooks()
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return RedirectToAction("LoginPage", "Home");
            }
            var books = await _adminService.GetAllBooksAsync(); 
            var categories = await _adminService.GetAllCategoriesAsync(); 
            var model = new BookUserViewModel
            {
                Books = books,
                Categories = categories 
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            var success = await _adminService.DeleteCategoryAsync(categoryId);
            if (success)
            {
                // De categorie is succesvol verwijderd
                return Json(new { success = true, message = "Categorie succesvol verwijderd." });
            }
            else
            {
                // Er is een fout opgetreden bij het verwijderen van de categorie
                return Json(new { success = false, message = "Er is een fout opgetreden bij het verwijderen van de categorie." });
            }
        }


        [HttpPost]
		public async Task<IActionResult> EditBook(BookDTO book)
		{
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return RedirectToAction("LoginPage", "Home");
            }
            var success = await _adminService.EditBookAsync(book);
			if (success)
			{
				TempData["SuccessMessage"] = "Book successfully edited.";
			}
			else
			{
				TempData["ErrorMessage"] = "An error occurred while editing the book.";
			}
			return RedirectToAction("ManageBooks");
		}

		[HttpPost]
		public async Task<IActionResult> DeleteBook(int bookId, int userId)
		{
			var success = await _adminService.DeleteBookAsync(bookId, userId);
			if (success)
			{
				TempData["SuccessMessage"] = "Book successfully deleted.";
			}
			else
			{
				TempData["ErrorMessage"] = "An error occurred while deleting the book.";
			}
			return RedirectToAction("ManageBooks");
		}

	}
}
