namespace CoffeeApplication
{
    public class AppSettings
    {
        public CoffeeSettings CoffeeSettings { get; set; }
    }

    public class CoffeeSettings
    {
        public string? CoffeeApiUrl { get; set; }
        public string ApiKey { get; set; }
        public string AuthenticationUsername { get; set; }
    }
}
