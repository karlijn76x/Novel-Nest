using Microsoft.AspNetCore.Mvc;
using Models;
using Novel_Nest.Models;
using Novel_Nest_Core;
using static System.Reflection.Metadata.BlobBuilder;

namespace Novel_Nest.Controllers
{
	public class NightstandController : Controller
	{
		private BookLogic _bookLogic;
		public NightstandController()
		{
			_bookLogic = new BookLogic();
		}

		public IActionResult Index()
		{
			var nightstandBooks = _bookLogic.GetNightstandBooks();
			var model = new SpecifyBookDetailsViewModel
			{
				NightstandBooks = nightstandBooks
			};
			return View(model);
		}


        [HttpGet]
        public IActionResult AddBookToNightstand()
        {
            var books = _bookLogic.GetBooks();
            var nightstandBooks = _bookLogic.GetNightstandBooks();

            var availableBooks = books.Where(book => !nightstandBooks.Any(nb => nb.BookId == book.Id)).ToList();

            var model = new SpecifyBookDetailsViewModel
            {
                Books = availableBooks,
                NightstandBooks = nightstandBooks
            };

            return View(model);
        }

        [HttpPost]
		public async Task<IActionResult> AddBookToNightstand(NightstandBookDTO nightstandBook)
		{
			if (ModelState.IsValid)
			{
				bool success = await _bookLogic.AddBookToNightstandAsync(nightstandBook);

				if (success)
				{
					
					return RedirectToAction("Index", "Nightstand");
				}
				else
				{
					ViewBag.ErrorMessage = "Failed to add book to nightstand. Please try again.";
					return View("ErrorView", nightstandBook);
				}
			}
			else
			{
				return View("AddBookToNightstand", nightstandBook);
			}
		}

        [HttpPost]
        public async Task<IActionResult> DeleteBook(int Id)
        {
            Console.WriteLine($"Attempting to delete book with Id: {Id}");
            bool success = await _bookLogic.DeleteNightstandBookAsync(Id);
            if (success)
            {
                return RedirectToAction("Index", "Nightstand");
            }
            else
            {
                ViewBag.ErrorMessage = "Failed to remove book from nightstand. Please try again.";
                return View("ErrorView");
            }
        }



    }

}

