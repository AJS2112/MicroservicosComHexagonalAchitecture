using Application.Booking.Dtos;

namespace Application.Payment.Ports
{
    public interface IPaymentProcessor
    {
        Task<string> CapturePayment(PaymentRequestDTO paymentRequestDTO);
    }
}