using Microsoft.AspNetCore.Mvc;
using Models;
using Novel_Nest.Models;
using Novel_Nest_Core;

public class NightstandController : Controller
{
	private readonly BookService _bookService;

	public NightstandController(BookService bookService)
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

		var nightstandBooks = _bookService.GetNightstandBooks(userId.Value);
		var model = new SpecifyBookDetailsViewModel
		{
			NightstandBooks = nightstandBooks
		};
		return View(model);
	}

	[HttpGet]
	public IActionResult AddBookToNightstand()
	{
		var userId = HttpContext.Session.GetInt32("UserId");
		if (userId == null)
		{
			return RedirectToAction("LoginPage", "Home");
		}

		var books = _bookService.GetBooks(userId.Value);
		var nightstandBooks = _bookService.GetNightstandBooks(userId.Value);

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
		var userId = HttpContext.Session.GetInt32("UserId");
		if (userId == null)
		{
			return RedirectToAction("LoginPage", "Home");
		}

		nightstandBook.UserId = userId.Value; // Voeg deze regel toe

		if (ModelState.IsValid)
		{
			bool success = await _bookService.AddBookToNightstandAsync(nightstandBook);

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
		var userId = HttpContext.Session.GetInt32("UserId");
		if (userId == null)
		{
			return RedirectToAction("LoginPage", "Home");
		}

		bool success = await _bookService.DeleteNightstandBookAsync(Id);
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
