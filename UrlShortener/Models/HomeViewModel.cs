using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrlShortener.Models
{
    public class HomeViewModel
    {
        public string LongUrl { get; set; }
        public string ShortUrlHash { get; set; }
    }
}
