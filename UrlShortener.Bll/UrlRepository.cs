using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlShortener.EntityFrameworkCore.Models;

namespace UrlShortener.Bll
{
    public class UrlRepository
    {
        private readonly ILogger<UrlRepository> _logger;
        public UrlRepository(ILogger<UrlRepository> logger)
        {
            _logger = logger;
        }

        public async Task<bool> TryCreateShortUrls(IEnumerable<(string url, string urlHash)> mappings)
        {
            using (var db = new UrlShortenerContext())
            {
                var rows = mappings.Select(map => new UrlMapping(map.url, map.Item2));
                db.UrlMappings.AddRange(rows);

                try
                {
                    await db.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    // Fail gracefully by masquerading this critical production error, & log server exception so it can be debugged later
                    _logger.LogError(ex, ex.Message);
                    return false;
                }
                return true;
            }
        }

        public async Task<string> TryGetLongUrl(string shortUrlHash)
        {
            using (var db = new UrlShortenerContext())
            {
                return await db.UrlMappings
                    .Where(m => m.ShortUrlHash == shortUrlHash)
                    .Select(m => m.LongUrl)
                    .SingleOrDefaultAsync();
            }
        }
    }
}
