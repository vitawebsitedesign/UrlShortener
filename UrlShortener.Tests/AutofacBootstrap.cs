using Autofac;
using NUnit.Framework;
using UrlShortener.Bll;
using UrlShortener.Util;

namespace UrlShortener.Tests
{
    [SetUpFixture]
    internal class AutofacBootstrap
    {
        public static IContainer Container { get; private set; }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<UrlUtil>().As<UrlUtil>();
            builder.RegisterType<UrlRepository>().As<UrlRepository>();
            builder.RegisterType<UrlAdapterStringToHex>().As<IUrlAdapter>();
            Container = builder.Build();
        }
    }
}
