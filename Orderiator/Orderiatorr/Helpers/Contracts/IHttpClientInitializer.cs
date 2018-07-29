using System.Net.Http;

namespace Orderiatorr.Controllers
{
    public interface IHttpClientInitializer
    {
        HttpClient InitializeClient();
    }
}
