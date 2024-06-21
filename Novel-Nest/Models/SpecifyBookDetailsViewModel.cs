using Models;

namespace Novel_Nest.Models
{
	public class SpecifyBookDetailsViewModel
	{
		public List<BookModel> Books { get; set; }
		public List<CategoryModel> Categories { get; set; }
		public List<NightstandBookModel> NightstandBooks { get; set;}
		public NightstandBookModel Book { get; set; }
	}
}
