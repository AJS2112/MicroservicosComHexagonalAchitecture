using Application.Booking.Commands;
using Application.Booking.Dtos;
using Application.Booking.Ports;
using Application.Booking.Queries;
using Application.Booking.Responses;
using Application.Payment.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingManager _bookingManager;
        private readonly IMediator _mediator;

        public BookingController(IBookingManager bookingManager, IMediator mediator)
        {
            _bookingManager = bookingManager; 
            _mediator = mediator;   
        }

        [HttpPost]
        public async Task<ActionResult<BookingResponse>> Post(BookingDTO bookingDto)
        {
            var command = new CreateBookingCommand
            {
                BookingDTO = bookingDto
            };

            return await _mediator.Send(command);
        }

        [HttpPost]
        public async Task<ActionResult<PaymentStateDTO>> Pay(PaymentRequestDTO paymentRequestDTO)
        {
            var res = await _bookingManager.PayForABooking(paymentRequestDTO);

            if (!string.IsNullOrEmpty(res)) return Ok("Payment received with success!");

            return BadRequest("Fuck you");
        }

        public async Task<ActionResult<BookingResponse>> Get(int id)
        {
            var query = new GetBookingQuery
            {
                Id = id,
            };

            var response = await _mediator.Send(query);

            if (response.Success) return Ok(response);

            return BadRequest(500);
        }
    }
}
