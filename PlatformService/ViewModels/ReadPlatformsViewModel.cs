using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;

namespace PlatformService.ViewModels
{
    public class ReadPlatformsViewModel
    {
        [Required]
        [Min(1)]
        public int PageSize { get; set; }

        [Required]
        [Min(1)]
        public int PageNumber { get; set; }

        public List<KeyValuePair<string, string>>? SortInfo { get; set; }
        public string? NameFilterValue { get; set; }
        public string? PublisherFilterValue { get; set; }

        [Min(0)]
        public double? MaxCostFilterValue { get; set; }
    }
}