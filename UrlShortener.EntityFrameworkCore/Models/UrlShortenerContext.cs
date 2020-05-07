using Microsoft.EntityFrameworkCore;
using System;
using UrlShortener.EntityFrameworkCore.Util;

namespace UrlShortener.EntityFrameworkCore.Models
{
    public class UrlShortenerContext : DbContext
    {
        public DbSet<UrlMapping> UrlMappings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connStr = AppSettingsFacade.GetAppSettings()["UrlShortenerConnectionString"];
            if (string.IsNullOrWhiteSpace(connStr))
                throw new Exception("Please open appsettings.json & set the connection string");

            optionsBuilder.UseSqlServer(connStr);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UrlMapping>()
                .HasIndex(u => u.ShortUrlHash)
                .IsUnique();
        }
    }
}
