using UniversityTuitionPaymentV2.Model.Constants;

namespace UniversityTuitionPaymentV2.Source.Services
{
    public interface IMessageSenderService
    {
        Task SendMessageAsync(PaymentInfo paymentInfo);
    }
}
