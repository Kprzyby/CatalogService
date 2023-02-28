namespace ServiceBusPublisher.Models
{
    public class PlatformRemovedEvent : Event
    {
        public int PlatformId { get; set; }
    }
}