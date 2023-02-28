using System.ComponentModel.DataAnnotations;

namespace ServiceBusPublisher.Models
{
    public class PlatformRemovedEvent : Event
    {
        [Required]
        public int PlatformId { get; set; }
    }
}