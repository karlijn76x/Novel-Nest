using Microsoft.AspNetCore.Mvc;
using Models;
using Novel_Nest_Core;
using Novel_Nest.Models;

namespace Novel_Nest.Controllers
{
	public class BookshelfController : Controller
	{
		
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult AddBooks()
		{
			return View();
		}
	}
}
