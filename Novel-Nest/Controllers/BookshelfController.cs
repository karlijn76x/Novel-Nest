using Microsoft.AspNetCore.Mvc;
using Models;
using Novel_Nest_Core;
using Novel_Nest.Models;

namespace Novel_Nest.Controllers
{
	public class BookshelfController : Controller
	{
		private BookLogic _bookLogic;


		public BookshelfController()
		{
			_bookLogic = new BookLogic();
		}

		public BookLogic Get_bookLogic()
		{
			return _bookLogic;
		}

		

		public IActionResult Index( )
		{
			var books = _bookLogic.GetBooks(); 

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
