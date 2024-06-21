using Models;
namespace Novel_Nest.Models
{
    public class BookUserViewModel
    {
        public List<BookDTO> Books { get; set; }
        public List<CategoryDTO> Categories { get; set; }
        public string UserName { get; set; }

    }
}
