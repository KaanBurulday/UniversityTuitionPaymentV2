namespace UniversityTuitionPaymentV2.Source.Services
{
    public interface IMessageReceiverService
    {
        Task StartProcessingAsync(CancellationToken cancellationToken);
    }
}
