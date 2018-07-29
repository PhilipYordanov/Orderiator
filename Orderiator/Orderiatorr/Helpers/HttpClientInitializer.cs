using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Orderiatorr.Controllers
{
    public class HttpClientInitializer : IHttpClientInitializer
    {
        //private string _apiBaseURI = "http://localhost:5555";
        private readonly IConfiguration _configuration;

        public HttpClientInitializer(IConfiguration configuration )
        {
            this._configuration = configuration;
        }

        public HttpClient InitializeClient()
        {
            var client = new HttpClient();
            //Passing service base url   
           
            client.BaseAddress = new Uri(_configuration.GetValue<string>("apiBaseURI"));

            client.DefaultRequestHeaders.Clear();
            //Define request data format    
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
           
            return client;
        }
    }
}
