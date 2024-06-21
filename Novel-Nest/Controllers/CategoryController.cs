using Microsoft.AspNetCore.Mvc;
using Models;
using Novel_Nest.Models;
using Novel_Nest_Core;
using System.Threading.Tasks;

namespace Novel_Nest.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryModel category)
        {
            if (ModelState.IsValid)
            {
                var userId = HttpContext.Session.GetInt32("UserId");
                if (userId == null)
                {
                    return RedirectToAction("LoginPage", "Home");
                }

                category.UserId = userId.Value; 

                var success = await _categoryService.AddCategoryAsync(category);

                if (success)
                {
                    Console.WriteLine("success");
                    return RedirectToAction("Category", "Category");
                }
                else
                {
                    Console.WriteLine("failed");
                    ViewBag.ErrorMessage = "Failed to add category. Please try again.";
                    return View("ErrorView", category);
                }
            }
            else
            {
                Console.WriteLine("failed 2");
                return View("AddCategory", category);
            }
        }

        public IActionResult Category()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("LoginPage", "Home");
            }

            var categories = _categoryService.GetCategories(userId.Value);
            var model = new CategoryViewModel
            {
                Categories = categories
            };
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteCategory(int Id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("LoginPage", "Home");
            }

            try
            {
                bool isCategoryInUse = await _categoryService.IsCategoryInUseAsync(Id);
                if (isCategoryInUse)
                {
                    TempData["ErrorMessage"] = "Cannot delete category because it is in use.";
                    return RedirectToAction("Category");
                }

                var success = await _categoryService.DeleteCategoryAsync(Id, userId.Value);
                if (success)
                {
                    TempData["Message"] = "Category successfully deleted.";
                    return RedirectToAction("Category");
                }
                else
                {
                    TempData["ErrorMessage"] = "Failed to delete category.";
                    return RedirectToAction("Category");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Failed to delete category. " + ex.Message;
                return RedirectToAction("Category");
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditCategoryAsync(CategoryModel category)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Invalid data.";
                return RedirectToAction("Category");
            }

            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("LoginPage", "Home");
            }

            category.UserId = userId.Value;

            var success = await _categoryService.EditCategoryAsync(category);
            if (success)
            {
                TempData["Message"] = "Category successfully edited.";
                return RedirectToAction("Category");
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to edit category.";
                return RedirectToAction("Category");
            }
        }
    }
}
