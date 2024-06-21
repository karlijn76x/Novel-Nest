using Models;

namespace Novel_Nest.Models
{
	public class EditBookViewModel
	{
		public List<CategoryModel>? Categories { get; set; }
		public List<BookModel>? Books { get; set; }
		public int categoryId { get; set; }
	}
}
