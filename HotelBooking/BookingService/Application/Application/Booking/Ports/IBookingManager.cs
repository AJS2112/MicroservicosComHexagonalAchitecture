using Application.Booking.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Booking.Ports
{
    public interface IBookingManager
    {
        Task<BookingDTO> CreateBooking(BookingDTO bookingDto);
        Task<BookingDTO> GetBooking(int id);
        Task<string> PayForABooking(PaymentRequestDTO paymentRequestDTO);
    }
}
