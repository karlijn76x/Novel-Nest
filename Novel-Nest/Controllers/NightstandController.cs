using System.Net;
using Microsoft.AspNetCore.Mvc;
using Models;
using Novel_Nest.Models;
using Novel_Nest_Core;
using Novel_Nest_DAL;

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
	public async Task<IActionResult> AddBookToNightstand(NightstandBookModel nightstandBook)
	{
		var userId = HttpContext.Session.GetInt32("UserId");
		if (userId == null)
		{
			return RedirectToAction("LoginPage", "Home");
		}

		nightstandBook.UserId = userId.Value;

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
    [HttpGet]
    public async Task<IActionResult> EditNightstandBook(int bookId)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
        {
            return RedirectToAction("LoginPage", "Home");
        }

        var book = await _bookService.GetNightstandBookByUserIdAndBookId(userId.Value, bookId);
        if (book == null)
        {
            return NotFound();
        }

        var model = new ReviewNightstandBookViewModel
        {
            UserId = userId.Value,
            BookId = bookId,
			Title = book.Title,
            
        };

        return View(model);
    }


    [HttpPost]
    public async Task<IActionResult> ReviewNightstandBook(ReviewNightstandBookViewModel model)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
        {
            return RedirectToAction("LoginPage", "Home");
        }

        if (model.UserId != userId.Value)
        {
            return Unauthorized();
        }

        if (ModelState.IsValid)
        {
            var nightstandBook = new NightstandBookModel
            {
                UserId = model.UserId,
                BookId = model.BookId,
                Rating = model.Rating,
                Review = model.Review,
                DateFinished = model.DateFinished,
				Finished = true
            };

            bool success = await _bookService.ReviewNightstandBookAsync(nightstandBook);

            if (success)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ErrorMessage = "Failed to update the book review. Please try again.";
                return View("ErrorView");
            }
        }
        else
        {
            
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
        
                Console.WriteLine(error.ErrorMessage);
            }

       
            return View("EditNightstandBook", model);
        }
    }




}

