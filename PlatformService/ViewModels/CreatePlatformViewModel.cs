using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;

namespace PlatformService.ViewModels
{
    public class CreatePlatformViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Publisher { get; set; }

        [Required]
        [Min(0)]
        public double Cost { get; set; }
    }
}