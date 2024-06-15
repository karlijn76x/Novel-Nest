using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Models;
using Newtonsoft.Json;

namespace Novel_Nest_Core
{
    public class APIService
    {
        private readonly IHttpClientFactory _httpClientFactory;
       
        public APIService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<OpenLibrarySearchResponse> SearchBooksAsync(string query)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync($"https://openlibrary.org/search.json?q={query}");
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<OpenLibrarySearchResponse>(jsonResponse);
            }
            return null; // Or handle errors as appropriate
        }
     
        public string GetCoverImageUrl(string olid, string size = "M")
        {
            return $"https://covers.openlibrary.org/a/olid/{olid}-{size}.jpg";
        }

    }
}
