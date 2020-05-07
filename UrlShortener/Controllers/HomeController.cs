using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UrlShortener.Bll;
using UrlShortener.Models;
using UrlShortener.Util;

namespace UrlShortener.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUrlAdapter _urlAdapter;
        private readonly UrlUtil _urlUtil;
        private readonly UrlRepository _urlRepository;

        public HomeController(IUrlAdapter urlAdapter, UrlUtil urlUtil, UrlRepository urlRepository)
        {
            _urlAdapter = urlAdapter;
            _urlUtil = urlUtil;
            _urlRepository = urlRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(nameof(Index), new HomeViewModel());
        }

        [HttpGet]
        public async Task<IActionResult> TryGotoShortUrl(string shortUrlHash)
        {
            var hashValid = !string.IsNullOrWhiteSpace(shortUrlHash);
            if (hashValid)
            {
                var longUrl = await _urlRepository.TryGetLongUrl(shortUrlHash);
                if (longUrl != default)
                {
                    return Redirect(longUrl);
                }
            }

            // Redirect to home for invalid URL hashes
            return View(nameof(Index), new HomeViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(HomeViewModel model)
        {
            return View(nameof(Index), await GetModelWithShortUrls(model));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task<HomeViewModel> GetModelWithShortUrls(HomeViewModel model)
        {
            var urls = await TryGetUrls(model);
            if (ModelState.IsValid)
            {
                var websiteUrl = $"{Request.Scheme}://{Request.Host}";
                model.UrlMappings = await TryCreateShortUrls(websiteUrl, urls);
                if (model.UrlMappings == null)
                {
                    ModelState.AddModelError("", "This URL shortener service is temporary unavailable - please try again later");
                }
            }

            if (!model.UrlMappings.Any())
            {
                ModelState.AddModelError("", "No valid URL(s) were supplied. Please supply at least 1 valid URL.");
            }

            return model;
        }

        private async Task<IEnumerable<HomeUrlMappingViewModel>> TryCreateShortUrls(string websiteUrl, IEnumerable<string> urls)
        {
            var mappings = new List<(string url, string urlHash)>();

            foreach (var url in urls)
            {
                var urlWithProtocol = _urlUtil.GetUrlWithProtocol(url);
                var urlHash = _urlAdapter.TryGetUrlHash(urlWithProtocol);
                if (urlHash != null)
                {
                    mappings.Add((urlWithProtocol, urlHash));
                }
            }

            if (await _urlRepository.TryCreateShortUrls(mappings))
            {
                var viewModels = mappings.Select(m => new HomeUrlMappingViewModel(websiteUrl, m.url, m.urlHash));
                return viewModels;
            }
            return null;
        }

        private async Task<IEnumerable<string>> TryGetUrls(HomeViewModel model)
        {
            var csvUrls = await TryGetUrlsFromCsv(model.Csv);
            var textboxUrl = string.IsNullOrWhiteSpace(model.UrlTextbox)
                ? Enumerable.Empty<string>()
                : new List<string> { model.UrlTextbox };

            return csvUrls.Concat(textboxUrl);
        }

        private async Task<IEnumerable<string>> TryGetUrlsFromCsv(IFormFile csv)
        {
            if (csv != null)
            {
                using (var stream = csv.OpenReadStream())
                {
                    return await _urlUtil.GetUrlsFromCsv(stream);
                }
            }
            return Enumerable.Empty<string>();
        }
    }
}
