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
        private readonly CategoryService _categoryService;

        public BookController(BookService bookService, APIService apiService, CategoryService categoryService)
        {
            _bookService = bookService;
            _apiService = apiService;
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<IActionResult> NewBook()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("LoginPage", "Home");
            }

            var categories = await _categoryService.GetUserAndDefaultCategoriesAsync(userId.Value);

            var model = new CategoryViewModel
            {
                Categories = categories
            };
            return View(model);
        }

        [HttpPost]
		public async Task<IActionResult> AddBook(BookModel book)
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
		public async Task<IActionResult> EditBook()
		{
			var userId = HttpContext.Session.GetInt32("UserId");
			if (userId == null)
			{
				return RedirectToAction("LoginPage", "Home");
			}

			var categories = await _categoryService.GetUserAndDefaultCategoriesAsync(userId.Value);
			var books = _bookService.GetBooks(userId.Value); 

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
        public async Task<IActionResult> EditLibraryBook(BookModel book)
        {
            try
            {
                var userId = HttpContext.Session.GetInt32("UserId");
                if (userId == null)
                {
                    return RedirectToAction("LoginPage", "Home");
                }
                book.UserId = userId.Value; 
                bool success = await _bookService.EditLibraryBookAsync(book);
                if (success)
                {
                    TempData["Message"] = "Edited";
                    return RedirectToAction("EditBook");
                }
                else
                {
                    TempData["ErrorMessage"] = "Failed to edit. You may not have permission to edit this book.";
                    return RedirectToAction("EditBook");
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Failed to edit: " + ex.Message;
                return View("ErrorView");
            }
        }

        //[HttpGet]
        //public async Task<IActionResult> SearchBooks(string query)
        //{
        //    if (string.IsNullOrWhiteSpace(query))
        //    {
        //        return Json(new { success = false, message = "Query cannot be empty." });
        //    }

        //    var searchResult = await _apiService.SearchBooksAsync(query);
        //    if (searchResult == null || searchResult.Docs == null || !searchResult.Docs.Any())
        //    {
        //        return Json(new { success = false, message = "No books found." });
        //    }

        //    return Json(new { success = true, books = searchResult.Docs });
        //}


        //[HttpPost]
        //public async Task<IActionResult> AddBookFromSearch(string olid)
        //{
        //    var userId = HttpContext.Session.GetInt32("UserId");
        //    if (userId == null)
        //    {
        //        return Json(new { success = false, message = "User not logged in." });
        //    }

        //    var book = await _apiService.SearchBooksAsync(olid);
        //    if (book == null || book.Docs == null || !book.Docs.Any())
        //    {
        //        return Json(new { success = false, message = "Book not found." });
        //    }

        //    var firstBook = book.Docs.First();
        //    var addSuccess = await _bookService.AddBookFromSearchAsync(firstBook, userId.Value);
        //    if (addSuccess)
        //    {
        //        return Json(new { success = true, message = "Book added successfully." });
        //    }
        //    else
        //    {
        //        return Json(new { success = false, message = "Failed to add the book." });
        //    }
        //}

    }
}
