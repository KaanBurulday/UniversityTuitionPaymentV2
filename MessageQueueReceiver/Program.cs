using Azure.Messaging.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MessageQueueReceiver
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            // The client that owns the connection and can be used to create senders and receivers
            ServiceBusClient client;

            // The processor that reads and processes messages from the queue
            ServiceBusProcessor processor;

            string serviceBusConnectionString = "Endpoint=sb://unituitionpaymentsrvbus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=zJgGPu5LZbEeqvDuQ6nShsEcMIIdYSXiy+ASbG9syus=";
            string queueName = "unituitionpaymentqueue";

            async Task MessageHandler(ProcessMessageEventArgs args1)
            {
                string body = args1.Message.Body.ToString();
                Console.WriteLine($"Received: {body}");
                // Perform your business logic here
                // Complete the message. Message is deleted from the queue. 
                await args1.CompleteMessageAsync(args1.Message);
            }

            // Handle any errors when receiving messages
            Task ErrorHandler(ProcessErrorEventArgs args2)
            {
                Console.WriteLine(args2.Exception.ToString());
                return Task.CompletedTask;
            }

            // The Service Bus client types are safe to cache and use as a singleton for the lifetime
            // of the application, which is best practice when messages are being published or read
            // regularly.
            //
            // Set the transport type to AmqpWebSockets so that the ServiceBusClient uses port 443. 
            // If you use the default AmqpTcp, make sure that ports 5671 and 5672 are open.

            // TODO: Replace the <NAMESPACE-CONNECTION-STRING> and <QUEUE-NAME> placeholders
            var clientOptions = new ServiceBusClientOptions()
            {
                TransportType = ServiceBusTransportType.AmqpWebSockets
            };
            client = new ServiceBusClient(serviceBusConnectionString, clientOptions);

            // Create a processor that we can use to process the messages
            // TODO: Replace the <QUEUE-NAME> placeholder
            processor = client.CreateProcessor(queueName, new ServiceBusProcessorOptions());

            try
            {
                // Add handler to process messages
                processor.ProcessMessageAsync += MessageHandler;

                // Add handler to process any errors
                processor.ProcessErrorAsync += ErrorHandler;

                // Start processing 
                await processor.StartProcessingAsync();

                Console.WriteLine("Wait for a minute and then press any key to end the processing");
                Console.ReadKey();

                // Stop processing 
                Console.WriteLine("\nStopping the receiver...");
                await processor.StopProcessingAsync();
                Console.WriteLine("Stopped receiving messages");
            }
            finally
            {
                // Calling DisposeAsync on client types is required to ensure that network
                // resources and other unmanaged objects are properly cleaned up.
                await processor.DisposeAsync();
                await client.DisposeAsync();
            }
        }

    }
}
