using Models;

namespace Novel_Nest.Models
{
	public class EditBookViewModel
	{
		public List<CategoryDTO> Categories { get; set; }
		public List<BookDTO> Books { get; set; }
		public int categoryId { get; set; }

		
	}
}
