using Models;

namespace Novel_Nest.Models
{
	public class SpecifyBookDetailsViewModel
	{
		public List<BookDTO> Books { get; set; }
		public List<CategoryDTO> Categories { get; set; }
		public List<NightstandBookDTO> NightstandBooks { get; set;}
	}
}
