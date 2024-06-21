
namespace Models
{
	public class BookDTO
	{   
		public int Id {  get; set; }
		public string Title { get; set; } = string.Empty; 
		public string Author {  get; set; } = string.Empty ;
		public int CategoryId { get; set; }
		public string CategoryName { get; set; } = string.Empty;
		public DateTime ? DateStarted { get; set; }
		public DateTime? EndDate { get; set; }
		public int UserId { get; set; }
        public string CoverImageUrl { get; set; } = string.Empty;
        public DateTime? DateFinished { get; set; }
        public int? Rating { get; set; }
        public string Review { get; set; } = string.Empty;
        public bool Finished { get; set; } = false;
		public int BookId { get; set; }
		public string UserName { get; set; } = string.Empty;
    }
}
