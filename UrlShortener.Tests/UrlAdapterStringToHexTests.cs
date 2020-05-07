using Autofac;
using NUnit.Framework;
using UrlShortener.Util;

namespace UrlShortener.Tests
{
    [Parallelizable(ParallelScope.All)]
    public class UrlAdapterStringToHexTests
    {
        private readonly IUrlAdapter _urlAdapter;
        private readonly UrlUtil _urlUtil;

        public UrlAdapterStringToHexTests()
        {
            _urlAdapter = AutofacBootstrap.Container.Resolve<IUrlAdapter>();
            _urlUtil = AutofacBootstrap.Container.Resolve<UrlUtil>();
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(".co.kr")]
        [TestCase("www..google.com.au")]
        [TestCase("www.google..com.au")]
        [TestCase("www.google.com..au")]
        public void UrlAdapterStringToHex_TryGetUrlHash_ShouldReturnNullFor_InvalidUrl(string input)
        {
            Assert.Null(_urlAdapter.TryGetUrlHash(input));
        }

        [TestCase("http://www.google.com.au")]
        [TestCase("https://www.google.com.au")]
        [TestCase("www.google.com.au")]
        [TestCase("google.com")]
        [TestCase("google.com.au")]
        [TestCase("google.com.au?a=1&b=some_string")]
        [TestCase("internationaldomain.co.kr")]
        [TestCase("www.subdomain.somedomain.com")]
        [TestCase("subdomain.somedomain.com")]
        [TestCase("subsubdomain.subdomain.somedomain.com")]
        [TestCase("subsubdomain.subdomain.somedomain.com.au")]
        [TestCase("https://youtu.be/prY4IxjWrso?t=563")]    // Short URL for a short URL
        [TestCase("https://www.youtube.com/watch?v=prY4IxjWrso&feature=youtu.be&t=563")]    // Real-world complex URL
        public void UrlAdapterStringToHex_TryGetUrlHash_ShouldReturnHashFor_ValidUrl(string urlRaw)
        {
            var url = _urlUtil.GetUrlWithProtocol(urlRaw);
            var hash = _urlAdapter.TryGetUrlHash(url);
            Assert.False(string.IsNullOrWhiteSpace(hash));
        }
    }
}
