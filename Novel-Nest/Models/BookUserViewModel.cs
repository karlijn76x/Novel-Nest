using Models;
namespace Novel_Nest.Models
{
    public class BookUserViewModel
    {
        public List<BookModel> Books { get; set; }
        public List<CategoryModel> Categories { get; set; }
        public string UserName { get; set; }

    }
}
