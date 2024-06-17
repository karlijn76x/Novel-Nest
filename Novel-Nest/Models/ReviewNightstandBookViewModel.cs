namespace Novel_Nest.Models
{
    public class ReviewNightstandBookViewModel
    {
        public int UserId { get; set; }
        public DateTime? DateFinished { get; set; }
        public int? Rating { get; set; }
        public string Review { get; set; }
        public int BookId { get; set; }
        public string Title { get; set; } = string.Empty;
    }
}
