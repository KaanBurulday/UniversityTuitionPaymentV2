using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace UniversityTuitionPaymentV2.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "1-Midterm Controller")]
    public class MessagesController : ControllerBase
    {
        // Connection string to your Service Bus namespace
        private readonly string _connectionString = "Endpoint=sb://unituitionpaymentsrvbus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=zJgGPu5LZbEeqvDuQ6nShsEcMIIdYSXiy+ASbG9syus=";
        private readonly string _queueName = "unituitionpaymentqueue";

        /*[HttpPost]
        public async Task<IActionResult> ReceiveMessages()
        {
            await using var client = new ServiceBusClient(connectionString);
            await using var receiver = client.CreateReceiver(queueName);

            // Receive a single message
            ServiceBusReceivedMessage message = await receiver.ReceiveMessageAsync();

            if (message != null)
            {
                // Process the received message
                string messageBody = Encoding.UTF8.GetString(message.Body);
                // Your message processing logic here...

                // Complete the message to remove it from the queue
                await receiver.CompleteMessageAsync(message);

                return Ok($"Received and processed message: {messageBody}");
            }
            else
            {
                return NoContent(); // No message available
            }
        }*/
        [HttpPost("send")]
        public async Task<IActionResult> SendMessageAsync([FromBody] string messageBody)
        {
            await using (ServiceBusClient client = new ServiceBusClient(_connectionString))
            {
                ServiceBusSender sender = client.CreateSender(_queueName);
                ServiceBusMessage message = new ServiceBusMessage(Encoding.UTF8.GetBytes(messageBody));
                await sender.SendMessageAsync(message);
                return Ok("Message sent to the queue successfully.");
            }
        }

        [HttpGet("receive")]
        public async Task<IActionResult> ReceiveMessageAsync()
        {
            await using (ServiceBusClient client = new ServiceBusClient(_connectionString))
            {
                ServiceBusReceiver receiver = client.CreateReceiver(_queueName);
                ServiceBusReceivedMessage message = await receiver.ReceiveMessageAsync();
                if (message != null)
                {
                    string messageBody = Encoding.UTF8.GetString(message.Body);
                    await receiver.CompleteMessageAsync(message);
                    return Ok(messageBody);
                }
                else
                {
                    return NotFound("No messages available in the queue.");
                }
            }
        }
    }
}
