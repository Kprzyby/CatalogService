using Azure.Messaging.ServiceBus;
using ServiceBusPublisher.Enums;
using ServiceBusPublisher.Models;
using System.Text;
using System.Text.Json;

namespace ServiceBusPublisher
{
    public class PublisherService
    {
        #region Constructors

        public PublisherService(ServiceBusSender senderClient)
        {
            _senderClient = senderClient;
        }

        #endregion Constructors

        #region Properties

        private readonly ServiceBusSender _senderClient;

        #endregion Properties

        #region Methods

        public async Task<bool> PublishMessageAsync(Event platformEvent, EventType eventType)
        {
            try
            {
                ServiceBusMessage message = new ServiceBusMessage();
                message.ApplicationProperties.Add("serviceName", "Platform");
                message.ContentType = "application/json";
                message.Subject = eventType.ToString();

                string jsonMessageBody = JsonSerializer.Serialize(platformEvent, platformEvent.GetType());
                message.Body = new BinaryData(Encoding.UTF8.GetBytes(jsonMessageBody));

                await _senderClient.SendMessageAsync(message);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion Methods
    }
}