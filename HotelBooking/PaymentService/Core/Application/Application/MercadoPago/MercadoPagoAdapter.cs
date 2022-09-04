using Application.MercadoPago.Exceptions;
using Application.Payment.Dtos;
using Application.Payment.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payments.Application.MercadoPago
{
    public class MercadoPagoAdapter : IPaymentProcessor
    {
        public Task<PaymentStateDTO> PayWithCreditCard(string paymentIntention)
        {
            if (string.IsNullOrEmpty(paymentIntention))
            {
                throw new InvalidPaymentIntentionException("Your Payment Exception was Invalid!!");
            }

            paymentIntention += "/success";

            var response = new PaymentStateDTO
            {
                CreateDate = DateTime.Now,
                Message = $"Yeah {paymentIntention}",
                PaymentId = "123",
                Status = Status.Success
            };

            return Task.FromResult(response);
        }

        public Task<PaymentStateDTO> PayWithDebitCard(string paymentIntention)
        {
            throw new NotImplementedException();
        }

        public Task<PaymentStateDTO> PayWithTransfer(string paymentIntention)
        {
            throw new NotImplementedException();
        }
    }
}
