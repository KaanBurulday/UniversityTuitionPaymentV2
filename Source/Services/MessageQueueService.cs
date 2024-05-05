using Azure.Messaging.ServiceBus;

namespace UniversityTuitionPaymentV2.Source.Services
{
    using Azure.Messaging.ServiceBus;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using System.Text;
    using System.Threading.Tasks;
    using UniversityTuitionPaymentV2.Model;

    public class MessageQueueService : IMessageQueueService
    {
        private readonly string _connectionString;
        private readonly string _queueName;

        public MessageQueueService(string connectionString, string queueName)
        {
            _connectionString = connectionString;
            _queueName = queueName;
        }

        public async Task SendNotificationAsync(string msg)
        {
            string msgJson = JsonConvert.SerializeObject(msg);

            await using (ServiceBusClient client = new ServiceBusClient(_connectionString))
            {
                ServiceBusSender sender = client.CreateSender(_queueName);
                ServiceBusMessage message = new ServiceBusMessage(Encoding.UTF8.GetBytes(msgJson));
                await sender.SendMessageAsync(message);
            }
        }

        public async Task<string> ReceiveNotificationAsync()
        {
            await using (ServiceBusClient client = new ServiceBusClient(_connectionString))
            {
                ServiceBusReceiver receiver = client.CreateReceiver(_queueName);
                ServiceBusReceivedMessage message = await receiver.ReceiveMessageAsync();
                //string studentJson = Encoding.UTF8.GetString(message.Body);
                //Student student = JsonConvert.DeserializeObject<Student>(studentJson);
                await receiver.CompleteMessageAsync(message);
                return Encoding.UTF8.GetString(message.Body);
            }
        }

    }
}
