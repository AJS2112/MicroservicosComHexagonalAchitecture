using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum SupportedPaymentProviders
{
    Paypal = 1,
    MercadoPago = 2,
    PagoSeguro = 3,
    Stripe = 4
}

public enum SupportedPaymentMethods
{
    DebitCard = 1,
    CreditCard = 2,
    BankTransfer = 3
}

namespace Application.Booking.Dtos
{
    public class PaymentRequestDTO
    {
        public int BookingId { get; set; }
        public string PaymentIntention { get; set; }
        public SupportedPaymentProviders PaymentProvider { get; set; }
        public SupportedPaymentMethods PaymentMethod { get; set; }
    }
}
