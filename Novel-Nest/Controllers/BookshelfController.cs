using Microsoft.AspNetCore.Mvc;
using Models;
using Novel_Nest_Core;
using Novel_Nest.Models;

namespace Novel_Nest.Controllers
{
    public class BookshelfController : Controller
    {
        private readonly BookService _bookService;

        public BookshelfController(BookService bookService)
        {
            _bookService = bookService;
        }

        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
			{
				return RedirectToAction("LoginPage", "Home");
			}
            var books = _bookService.GetBooks(userId.Value);

            var model = new BooksViewModel
            {
                Books = books
            };
            return View(model);
        }

        public IActionResult AddBooks()
        {
            return View();
        }

        public IActionResult EditBook()
        {
            return View();
        }
    }
}
