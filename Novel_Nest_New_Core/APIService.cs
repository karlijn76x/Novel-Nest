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
