using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;

namespace ServiceBusPublisher.Models
{
    public class PlatformUpdatedEvent : Event
    {
        [Required]
        public int PlatformId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Publisher { get; set; }

        [Required]
        [Min(0)]
        public double Cost { get; set; }
    }
}