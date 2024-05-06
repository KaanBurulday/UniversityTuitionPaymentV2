using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System.Text;
using UniversityTuitionPaymentV2.Model.Constants;

namespace UniversityTuitionPaymentV2.Source.Services
{
    public class MessageSenderService : IMessageSenderService
    {
        private readonly ServiceBusSender _sender;

        public MessageSenderService(string connectionString, string queueName)
        {
            _sender = new ServiceBusClient(connectionString).CreateSender(queueName);
        }

        public async Task SendMessageAsync(PaymentInfo paymentInfo)
        {
            string messageBody = JsonConvert.SerializeObject(paymentInfo);
            ServiceBusMessage message = new ServiceBusMessage(Encoding.UTF8.GetBytes(messageBody));

            await _sender.SendMessageAsync(message);
        }
    }
}
