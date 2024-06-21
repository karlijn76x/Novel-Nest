using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    //Wordt momenteel niet gebruikt, dit is voor als er een externe API wordt gebruikt om boeken toe te voegen of op te zoeken.
    public class OpenLibrarySearchResponse
    {
        public int NumFound { get; set; }
        public List<OpenLibraryBook> Docs { get; set; }
    }
    public class OpenLibraryBook
    {
        public string Title { get; set; }
        public List<string> AuthorName { get; set; }
        public string CoverImageUrl { get; set; }
        public string Olid { get; set; }
    }


}
