using Microsoft.EntityFrameworkCore;

namespace UrlShortener.EntityFrameworkCore.Models
{
    public class UrlShortenerContext : DbContext
    {
        public DbSet<UrlMapping> UrlMappings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UrlMapping>()
                .HasIndex(u => u.ShortUrlHash)
                .IsUnique();
        }
    }
}
