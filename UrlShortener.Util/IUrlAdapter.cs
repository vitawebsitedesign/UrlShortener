namespace UrlShortener.Util
{
    public interface IUrlAdapter
    {
        string GetHashedUrl(string input);
    }
}
