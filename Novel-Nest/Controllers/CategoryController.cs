using Microsoft.AspNetCore.Mvc;
using Models;
using Novel_Nest.Models;
using Novel_Nest_Core;

namespace Novel_Nest.Controllers
{
	public class CategoryController : Controller
	{
		 private CategoryLogic _categoryLogic;
		[HttpPost]
		public async Task<IActionResult> AddCategory(CategoryDTO category)
		{

			if (ModelState.IsValid)
			{

				bool success = true;

				if (success)
				{

					Console.WriteLine("success");

					_categoryLogic = new CategoryLogic();

					_categoryLogic.AddCategory(category);

					return RedirectToAction("Category", "Category");
				}
				else
				{
					Console.WriteLine("failed");

					ViewBag.ErrorMessage = "Failed to create account. Please try again.";
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
            _categoryLogic = new CategoryLogic();
            var model = new CategoryViewModel
            {
                Categories = _categoryLogic.GetCategories()
            };
            return View(model);
        }
    }
}
