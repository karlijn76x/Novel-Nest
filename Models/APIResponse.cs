using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class OpenLibrarySearchResponse
    {
        public int NumFound { get; set; }
        public List<OpenLibraryBook> Docs { get; set; }
    }

    public class OpenLibraryBook
    {
        // Add properties as per your requirement
        public string Title { get; set; }
        public List<string> Author { get; set; }
        public string CoverImageUrl { get; set; }
        public string Olid { get; set; }
    }

}
