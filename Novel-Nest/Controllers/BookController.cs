using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;
using Novel_Nest.Models;
using Novel_Nest_DAL;

namespace Novel_Nest.Controllers
{
	public class BookController : Controller
	{
        private IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public IActionResult NewBook()
		{
            var categories = _bookRepository.GetCategories(); 

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

                bool success = true;

                if (success)
                {

                    Console.WriteLine("success");

                    _bookRepository = new BookLogic();

                    _bookRepository.AddBookAsync(book);

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
            var categories = _bookRepository.GetCategories();
            var books = _bookRepository.GetBooks();

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
                await _bookRepository.DeleteBookAsync(Id);
                return RedirectToAction("EditBook");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Failed to delete" + ex.Message;
                return View("ErrorView");
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditBook( BookDTO book)
        {
            try
            {
                await _bookRepository.EditBookAsync(book);
                return RedirectToAction("EditBook");
            }
            catch(Exception ex)
            {
				ViewBag.ErrorMessage = "Failed to edit" + ex.Message;
				return View("ErrorView");
			}
        }
    }
}
