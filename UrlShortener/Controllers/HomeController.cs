using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UrlShortener.Bll;
using UrlShortener.Models;
using UrlShortener.Util;

namespace UrlShortener.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index(HomeViewModel viewModel)
        {
            viewModel = viewModel ?? new HomeViewModel();
            return View(nameof(Index), viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> TryGotoShortUrl(string shortUrlHash)
        {
            var shouldRedirect = !string.IsNullOrWhiteSpace(shortUrlHash);
            if (shouldRedirect)
            {
                var repo = new UrlRepository();
                var longUrl = await repo.TryGetLongUrl(shortUrlHash);
                if (longUrl != default)
                {
                    return Redirect(longUrl);
                }
            }
            return Index(null);
        }

        [HttpPost]
        public async Task<IActionResult> ShortenUrl(HomeViewModel model)
        {
            var repo = new UrlRepository();
            var adapter = new UrlAdapterStringToHex();
            var shortUrl = adapter.GetHashedUrl(model.LongUrl);
            await repo.CreateUrlMapping(model.LongUrl, shortUrl);

            return Index(new HomeViewModel
            {
                LongUrl = model.LongUrl,
                ShortUrlHash = shortUrl
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
