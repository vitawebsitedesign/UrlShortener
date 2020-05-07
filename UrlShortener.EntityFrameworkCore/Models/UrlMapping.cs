using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UrlShortener.EntityFrameworkCore.Models
{
    public class UrlMapping
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string LongUrl { get; set; }
        [Required]
        public string ShortUrlHash { get; set; }

        public UrlMapping(string longUrl, string shortUrlHash)
        {
            LongUrl = longUrl;
            ShortUrlHash = shortUrlHash;
        }
    }
}
