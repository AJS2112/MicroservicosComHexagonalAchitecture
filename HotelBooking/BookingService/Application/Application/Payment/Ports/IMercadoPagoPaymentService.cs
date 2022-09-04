using Application.Payment.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Payment.Ports
{
    public interface IMercadoPagoPaymentService
    {
        Task<PaymentStateDTO> PayWithCreditCard(string paymentIntention);
        Task<PaymentStateDTO> PayWithDebitCard(string paymentIntention);
        Task<PaymentStateDTO> PayWithTransfer(string paymentIntention);
    }
}
