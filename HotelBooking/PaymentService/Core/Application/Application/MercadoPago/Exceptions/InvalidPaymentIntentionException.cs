using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MercadoPago.Exceptions
{
    public class InvalidPaymentIntentionException : Exception
    {
        public InvalidPaymentIntentionException() { }
        public InvalidPaymentIntentionException(string message) : base(message) { }
        public InvalidPaymentIntentionException(string message, Exception inner) : base(message, inner) { }
        protected InvalidPaymentIntentionException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
