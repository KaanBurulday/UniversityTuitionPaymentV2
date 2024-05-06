using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System.Text;
using UniversityTuitionPaymentV2.Model.Constants;

namespace UniversityTuitionPaymentV2.Source.Services
{
    public class MessageReceiverService : IMessageReceiverService
    {
        private readonly ServiceBusProcessor _processor;

        public MessageReceiverService(string connectionString, string queueName)
        {
            _processor = new ServiceBusClient(connectionString)
                .CreateProcessor(queueName, new ServiceBusProcessorOptions());
        }

        public async Task StartProcessingAsync(CancellationToken cancellationToken)
        {
            _processor.ProcessMessageAsync += ProcessMessageAsync;
            _processor.ProcessErrorAsync += ProcessErrorAsync;

            await _processor.StartProcessingAsync(cancellationToken);
        }

        private async Task ProcessMessageAsync(ProcessMessageEventArgs args)
        {
            string json = Encoding.UTF8.GetString(args.Message.Body);
            PaymentInfo paymentInfo = JsonConvert.DeserializeObject<PaymentInfo>(json);

            // Here you can implement your notification sending logic
            // For simplicity, let's just log the received paymentInfo
            Console.WriteLine($"Received PaymentInfo: {paymentInfo}");

            await args.CompleteMessageAsync(args.Message);
        }

        private Task ProcessErrorAsync(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }
    }
}
