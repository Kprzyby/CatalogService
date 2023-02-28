namespace ServiceBusPublisher.Models
{
    public abstract class Event
    {
        #region Constructors

        public Event()
        {
            EventId = Guid.NewGuid();
            CreatedDate = DateTime.Now;
        }

        #endregion Constructors

        #region Properties

        public Guid EventId { get; set; }
        public DateTime CreatedDate { get; set; }

        #endregion Properties
    }
}