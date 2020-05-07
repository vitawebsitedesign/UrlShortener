using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;

namespace UrlShortener.Models
{
    public class HomeViewModel
    {
        [DisplayName("URL")]
        [DataType(DataType.Url)]
        [DataAnnotationsExtensions.Url(UrlOptions.OptionalProtocol, ErrorMessage = "Please input a valid URL")]
        public string UrlTextbox { get; set; }
        public IEnumerable<HomeUrlMappingViewModel> UrlMappings { get; set; } = new List<HomeUrlMappingViewModel>();
        public IFormFile Csv { get; set; }
    }
}
