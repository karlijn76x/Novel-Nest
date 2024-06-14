using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;
using Novel_Nest.Models;
using Novel_Nest_Core;
using System.Threading.Tasks;

namespace Novel_Nest.Controllers
{
    public class BookController : Controller
    {
        private readonly BookService _bookService;

        public BookController(BookService bookService)
        {
            _bookService = bookService;
        }

        public IActionResult NewBook()
        {
            var categories = _bookService.GetCategories();

            var model = new CategoryViewModel
            {
                Categories = categories
            };
            return View(model);
        }

		[HttpPost]
		public async Task<IActionResult> AddBook(BookDTO book)
		{
			if (ModelState.IsValid)
			{
				var userId = HttpContext.Session.GetInt32("UserId");
				if (userId == null)
				{
					return RedirectToAction("LoginPage", "Home");
				}
				book.UserId = userId.Value; // Voeg deze regel toe
				bool success = await _bookService.AddBookAsync(book);
				if (success)
				{
					return RedirectToAction("Index", "Bookshelf");
				}
				else
				{
					ViewBag.ErrorMessage = "Failed to create book. Please try again.";
					return View("ErrorView", book);
				}
			}
			else
			{
				return View("AddCategory", book);
			}
		}


		[HttpGet]
		public IActionResult EditBook()
		{
			var userId = HttpContext.Session.GetInt32("UserId");
			if (userId == null)
			{
				return RedirectToAction("LoginPage", "Home");
			}

			var categories = _bookService.GetCategories();
			var books = _bookService.GetBooks(userId.Value); // Gebruik de userId om de boeken op te halen

			var model = new EditBookViewModel
			{
				Categories = categories,
				Books = books
			};

			return View(model);
		}


		[HttpPost]
        public async Task<IActionResult> DeleteBook(int Id)
        {
            try
            {
                await _bookService.DeleteBookAsync(Id);
                return RedirectToAction("EditBook");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Failed to delete: " + ex.Message;
                return View("ErrorView");
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditBook(BookDTO book)
        {
            try
            {
                await _bookService.EditBookAsync(book);
                return RedirectToAction("EditBook");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Failed to edit: " + ex.Message;
                return View("ErrorView");
            }
        }
    }
}
