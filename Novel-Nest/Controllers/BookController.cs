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
                bool success = await _bookService.AddBookAsync(book);
                if (success)
                {
                    Console.WriteLine("success");
                    return RedirectToAction("Index", "Bookshelf");
                }
                else
                {
                    Console.WriteLine("failed");
                    ViewBag.ErrorMessage = "Failed to create account. Please try again.";
                    return View("ErrorView", book);
                }
            }
            else
            {
                Console.WriteLine("failed 2");
                return View("AddCategory", book);
            }
        }

        [HttpGet]
        public IActionResult EditBook()
        {
            var categories = _bookService.GetCategories();
            var books = _bookService.GetBooks();

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
