using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace UrlShortener.Util
{
    public class UrlUtil
    {
        private readonly IUrlAdapter _urlAdapter;

        public UrlUtil(IUrlAdapter urlAdapter)
        {
            _urlAdapter = urlAdapter;
        }

        public string GetUrlWithProtocol(string url)
        {
            return url.StartsWith("http") ? url : $"https://{url}";    // Assume https if user didn't specify a URL protocol
        }

        public async Task<IEnumerable<string>> GetUrlsFromCsv(Stream stream)
        {
            var urls = new List<string>();

            using (var streamReader = new StreamReader(stream))
            using (var csvReader = new CsvReader(streamReader, CultureInfo.CurrentUICulture))
            {
                var checkingHeader = true;
                while (await csvReader.ReadAsync())
                {
                    var url = csvReader.GetField<string>(0);
                    if (checkingHeader)
                    {
                        checkingHeader = false;
                        var isHeaderRow = _urlAdapter.TryGetUrlHash(url) == null;
                        if (isHeaderRow)
                        {
                            continue;
                        }
                    }
                    urls.Add(url);
                }
            }

            return urls;
        }
    }
}
