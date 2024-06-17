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
        private readonly APIService _apiService;

        public BookController(BookService bookService, APIService apiService)
        {
            _bookService = bookService;
            _apiService = apiService;
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
				book.UserId = userId.Value;
				bool success = await _bookService.AddBookAsync(book);
				if (success)
				{
					TempData["Message"] = "Book successfully added.";
					return RedirectToAction("NewBook");
				}
				else
				{
					TempData["ErrorMessage"] = "Failed to create book. Please try again.";
					return RedirectToAction("NewBook");
				}
			}
			else
			{
				TempData["ErrorMessage"] = "Invalid book data. Please try again.";
				return RedirectToAction("NewBook");
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
                bool isInNightstand = await _bookService.IsBookInNightstandAsync(Id);
                if (isInNightstand)
                {
                    TempData["Message"] = "CannotDeleteInNightstand"; // Specifieke sleutel voor deze situatie
                    return RedirectToAction("EditBook");
                }
                await _bookService.DeleteBookAsync(Id);
                TempData["Message"] = "Deleted";
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
                TempData["Message"] = "Edited";
                return RedirectToAction("EditBook");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Failed to edit: " + ex.Message;
                return View("ErrorView");
            }
        }

        [HttpPost]
        public async Task<IActionResult> SearchBooks(string query)
        {
            var searchResults = await _apiService.SearchBooksAsync(query);
            var books = searchResults.Docs.Select(book => new
            {
                Title = book.Title,
                // Controleer of de Author lijst null is en vervang deze door een lege lijst of standaardwaarde indien nodig
                Author = book.Author != null ? string.Join(", ", book.Author) : "Onbekende auteur",
                CoverImageUrl = _apiService.GetCoverImageUrl(book.Olid),
            }).ToList();

            return Json(books);
        }


    }
}
