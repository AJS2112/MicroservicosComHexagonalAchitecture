using Application.Booking.Dtos;
using Application.Booking.Ports;
using Application.Payment.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingManager _bookingManager;
        public BookingController(IBookingManager bookingManager)
        {
            _bookingManager = bookingManager; 
        }

        [HttpPost]
        public async Task<ActionResult<BookingDTO>> Post(BookingDTO bookingDto)
        {
            return await _bookingManager.CreateBooking(bookingDto);
        }

        [HttpPost]
        public async Task<ActionResult<PaymentStateDTO>> Pay(PaymentRequestDTO paymentRequestDTO)
        {
            var res = await _bookingManager.PayForABooking(paymentRequestDTO);

            if (res) return Ok("Payment received with success!");

            return BadRequest("Fuck you");
        }
    }
}
