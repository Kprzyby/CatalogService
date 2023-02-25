namespace PlatformService.Data.DTOs
{
    public class PlatformsFilteringDTO
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public List<KeyValuePair<string, string>>? SortInfo { get; set; }
        public string? NameFilterValue { get; set; }
        public string? PublisherFilterValue { get; set; }
        public double? MaxCostFilterValue { get; set; }
    }
}