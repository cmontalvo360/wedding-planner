using System;
using System.ComponentModel.DataAnnotations;
namespace Wedding_Planner.Models
{
    public class WeddingViewModel
    {
        [Required]
        [MinLength(2, ErrorMessage = "Must be more than 1 letter")]
        public string WedderOne {get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "Must be more than 1 letter")]
        public string WedderTwo {get; set; }
        [Required]
        public DateTime WeddDate {get; set; }
        [Required]
        public string WedAddress {get; set; }
    }
}