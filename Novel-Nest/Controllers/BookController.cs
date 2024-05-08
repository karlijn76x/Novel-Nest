using Microsoft.AspNetCore.Mvc;
using Models;
using Novel_Nest.Models;
using Novel_Nest_Core;

namespace Novel_Nest.Controllers
{
	public class BookController : Controller
	{
        private BookLogic _bookLogic;
       

        public BookController()
        {
            _bookLogic = new BookLogic(); 
            
        }

        public IActionResult NewBook()
		{
            var categories = _bookLogic.GetCategories(); 

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

                    _bookLogic = new BookLogic();

                    _bookLogic.AddBookAsync(book);

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
            var categories = _bookLogic.GetCategories();
            var books = _bookLogic.GetBooks();

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
                await _bookLogic.DeleteBookAsync(Id);
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
                await _bookLogic.EditBookAsync(book);
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
