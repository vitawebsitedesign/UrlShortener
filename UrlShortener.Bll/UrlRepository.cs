using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using UrlShortener.EntityFrameworkCore.Models;

namespace UrlShortener.Bll
{
    public class UrlRepository
    {
        public async Task<bool> CreateUrlMapping(string longUrl, string shortUrl)
        {
            if (string.IsNullOrWhiteSpace(longUrl))
                throw new ArgumentException(nameof(longUrl));
            if (string.IsNullOrWhiteSpace(shortUrl))
                throw new ArgumentException(nameof(shortUrl));

            using (var db = new UrlShortenerContext())
            {
                db.UrlMappings.Add(new UrlMapping(longUrl, shortUrl));
                try
                {
                    await db.SaveChangesAsync();
                }
                catch
                {
                    // I would do logging here for the exception (e.g.: log4net, azure app insights etc)
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
