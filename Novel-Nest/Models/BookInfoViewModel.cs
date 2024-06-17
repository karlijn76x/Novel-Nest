using Models;

namespace Novel_Nest.Models
{
    public class BookInfoViewModel
    {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Author { get; set; }
            public int CategoryId { get; set; }
            public string CategoryName { get; set; }
            public DateTime? DateStarted { get; set; }
            public DateTime? DateFinished { get; set; }
            public int? Rating { get; set; }
            public string Review { get; set; }
            public bool Finished { get; set; }
            public string CoverImageUrl { get; set; }

            public BookInfoViewModel(BookDTO book)
            {
                Id = book.Id;
                Title = book.Title;
                Author = book.Author;
                CategoryId = book.CategoryId;
                CategoryName = book.CategoryName;
                DateStarted = book.DateStarted;
                DateFinished = book.DateFinished;
                Rating = book.Rating;
                Review = book.Review;
                Finished = book.Finished;
                CoverImageUrl = book.CoverImageUrl; 
            }

    }
}
