using System;

namespace UrlShortener.Util
{
    public class UrlAdapterStringToHex : IUrlAdapter
    {
        public string GetHashedUrl(string input)
        {
            if (Uri.TryCreate(input, UriKind.Absolute, out var longUri))
            {
                if (longUri.Scheme == Uri.UriSchemeHttp || longUri.Scheme == Uri.UriSchemeHttps)
                {
                    var code = longUri.AbsoluteUri.GetHashCode();
                    var hash = string.Format("{0:X}", code);
                    return hash;
                }
            }
            return null;
        }
    }
}
