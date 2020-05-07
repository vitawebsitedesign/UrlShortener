using System;
using System.Collections.Generic;
using System.Linq;

namespace UrlShortener.Util
{
    public class UrlAdapterStringToHex : IUrlAdapter
    {
        public string TryGetUrlHash(string input)
        {
            var inputIsUrl = Uri.TryCreate(input, UriKind.Absolute, out var url)
                && (url.Scheme == Uri.UriSchemeHttp || url.Scheme == Uri.UriSchemeHttps);
            if (inputIsUrl)
            {
                var code = Guid.NewGuid().GetHashCode();
                var hash = string.Format("{0:X}", code);
                return hash;
            }
            return null;
        }
    }
}
