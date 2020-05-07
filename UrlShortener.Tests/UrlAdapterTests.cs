using NUnit.Framework;
using UrlShortener.Util;

namespace UrlShortener.Tests
{
    [Parallelizable(ParallelScope.All)]
    public class UrlAdapterTests
    {
        private IUrlAdapter _urlAdapter;

        [OneTimeSetUp]
        public void SetUp()
        {
            _urlAdapter = new UrlAdapterStringToHex();
        }

        // .co.kr
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("co.kr")]
        [TestCase(".co.kr")]
        public void UrlAdapter_GetHashedUrl_ShouldReturnNull_ForInvalidUrl(string input)
        {
            Assert.Null(_urlAdapter.GetHashedUrl(input));
        }

        // http://www.google.com.au
        [Test]
        public void UrlAdapter_GetHashedUrl_ShouldReturnValidHashFor_Http()
        {
            Assert.True(false);
        }

        // https://www.google.com.au
        [Test]
        public void UrlAdapter_GetHashedUrl_ShouldReturnValidHashFor_Https()
        {
            Assert.True(false);
        }

        // www.google.com.au
        [Test]
        public void UrlAdapter_GetHashedUrl_ShouldReturnValidHashFor_Www()
        {
            Assert.True(false);
        }

        // google.com.au
        [Test]
        public void UrlAdapter_GetHashedUrl_ShouldReturnValidHashFor_NoWww()
        {
            Assert.True(false);
        }

        // koreatimes.co.kr
        [Test]
        public void UrlAdapter_GetHashedUrl_ShouldReturnValidHashFor_Region()
        {
            Assert.True(false);
        }
    }
}
