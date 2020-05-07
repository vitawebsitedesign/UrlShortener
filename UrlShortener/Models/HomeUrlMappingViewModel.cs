namespace UrlShortener.Models
{
    public class HomeUrlMappingViewModel
    {
        public string Url { get; private set; }
        public string UrlShort { get; private set; }

        public HomeUrlMappingViewModel(string websiteUrl, string url, string urlHash)
        {
            Url = url;
            UrlShort = $"{websiteUrl}/{urlHash}";
        }
    }
}
