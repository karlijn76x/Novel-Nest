
namespace Models
{
	public class NightstandBookModel
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public int BookId { get; set; }
		public string Title { get; set; } = string.Empty;
		public string Author { get; set; } = string.Empty;
		public DateTime? DateStarted { get; set; }
		public DateTime? DateFinished { get; set; }
		public int? Rating { get; set; }
		public string Review { get; set; } = string.Empty;
		public bool Finished { get; set; } = false;
	}
}
