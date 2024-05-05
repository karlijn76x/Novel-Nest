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
    }
}
