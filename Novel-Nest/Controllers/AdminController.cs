using Microsoft.AspNetCore.Mvc;
using Models;
using Novel_Nest.Models;
using Novel_Nest_Core;

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
			var userCategories = await _adminService.GetAllCategoriesAsync();
			var defaultCategories = await _adminService.GetDefaultCategoriesAsync();

			var allCategories = userCategories.Concat(defaultCategories).ToList();

			var model = new BookUserViewModel
			{
				Books = books,
				Categories = allCategories 
			};
			return View(model);
		}


		[HttpPost]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            var success = await _adminService.DeleteCategoryAsync(categoryId);
            if (success)
            {
                return Json(new { success = true, message = "Categorie succesvol verwijderd." });
            }
            else
            {
                return Json(new { success = false, message = "Er is een fout opgetreden bij het verwijderen van de categorie." });
            }
        }

        [HttpPost]
		public async Task<IActionResult> EditBook(BookModel book)
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


        public async Task<IActionResult> AddDefaultCategories()
		{
			var defaultCategories = await _adminService.GetDefaultCategoriesAsync();
			var model = new DefaultCategoryViewModel
			{
				Category = defaultCategories
			};
			return View(model);
		}

		[HttpPost]
        public async Task<IActionResult> AddDefaultCategory(CategoryModel category)
        {
            if (ModelState.IsValid)
            { 
                category.UserId = null; 

                category.IsDefault = true; 

                var success = await _adminService.AddDefaultCategoryAsync(category);

                if (success)
                {
                    TempData["Message"] = "Default category successfully added.";
                    return RedirectToAction("AddDefaultCategories");
                }
                else
                {
                    TempData["ErrorMessage"] = "Failed to add default category. Please try again.";
                    return View("AddDefaultCategories", category);
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Invalid data.";
                return View("AddDefaultCategories", category);
            }
        }

		[HttpPost]
		public async Task<IActionResult> EditDefaultCategory(CategoryModel category)
		{
			var success = await _adminService.EditDefaultCategoryAsync(category);
			if (success)
			{
				TempData["Message"] = "Default category successfully edited.";
			}
			else
			{
				TempData["ErrorMessage"] = "Failed to edit default category. Please try again.";
			}
			return RedirectToAction("AddDefaultCategories");
		}

		[HttpPost]
		public async Task<IActionResult> DeleteDefaultCategory(int categoryId)
		{
			var success = await _adminService.DeleteDefaultCategoryAsync(categoryId);
			if (success)
			{
				TempData["Message"] = "Default category successfully deleted.";
			}
			else
			{
				TempData["ErrorMessage"] = "Failed to delete default category. Please try again.";
			}
			return RedirectToAction("AddDefaultCategories");
		}


	}
}
