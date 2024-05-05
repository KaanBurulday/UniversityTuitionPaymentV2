using UniversityTuitionPaymentV2.Model;

namespace UniversityTuitionPaymentV2.Source.Services
{
    public interface IMessageQueueService
    {
        Task SendNotificationAsync(string msg);
        Task<string> ReceiveNotificationAsync();
    }
}
