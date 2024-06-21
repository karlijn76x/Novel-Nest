
namespace Novel_Nest_Core
{
    //Wordt momenteel niet gebruikt, dit is voor als er een externe API wordt gebruikt om boeken toe te voegen of op te zoeken.
    public class APIService
    {
        private readonly IHttpClientFactory _httpClientFactory;
       
        public APIService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

    }
}
