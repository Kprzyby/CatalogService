using System.ComponentModel.DataAnnotations;

namespace PlatformService.Data.Entities
{
    public class Platform
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Publisher { get; set; }

        public double Cost { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}