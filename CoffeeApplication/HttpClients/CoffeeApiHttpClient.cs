namespace CoffeeApplication.HttpClients
{
    public class CoffeeApiHttpClient
    {
        public HttpClient HttpClient { get; }
        public CoffeeApiHttpClient(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }
    }
}
