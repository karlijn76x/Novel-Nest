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
        public async Task<IActionResult> AddCategory(CategoryDTO category)
        {
            if (ModelState.IsValid)
            {
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
            var categories = _categoryService.GetCategories();
            var model = new CategoryViewModel
            {
                Categories = categories
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCategory(int Id)
        {
            try
            {
                var success = await _categoryService.DeleteCategoryAsync(Id);
                if (success)
                {
                    return RedirectToAction("Category");
                }
                else
                {
                    ViewBag.ErrorMessage = "Failed to delete category.";
                    return View("ErrorView");
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Failed to delete category. " + ex.Message;
                return View("ErrorView");
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditCategoryAsync(CategoryDTO category)
        {
            try
            {
                var success = await _categoryService.EditCategoryAsync(category);
                if (success)
                {
                    return RedirectToAction("Category");
                }
                else
                {
                    ViewBag.ErrorMessage = "Failed to edit category.";
                    return View("ErrorView", category);
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Failed to edit category. " + ex.Message;
                return View("ErrorView", category);
            }
        }
    }
}
