using Application.Booking.Dtos;
using Application.Booking.Responses;
using Application.Payment.Ports;
using Domain.Ports;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Booking.Commands
{
    public class CreateBookingHandler : IRequestHandler<CreateBookingCommand, BookingResponse>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IGuestRepository _guestRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IPaymentProcessorFactory _paymentProcessorFactory;
        public CreateBookingHandler(IBookingRepository bookingRepository, IGuestRepository guestRepository, IRoomRepository roomRepository, IPaymentProcessorFactory paymentProcessorFactory)
        {
            _bookingRepository = bookingRepository;
            _guestRepository = guestRepository;
            _roomRepository = roomRepository;
            _paymentProcessorFactory = paymentProcessorFactory;
        }
    
        public async Task<BookingResponse> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var booking = BookingDTO.MapToEntity(request.BookingDTO);
                booking.Guest = await _guestRepository.Get(request.BookingDTO.GuestId);
                booking.Room = await _roomRepository.Get(request.BookingDTO.RoomId);

                await _bookingRepository.Create(booking);
                request.BookingDTO.Id = booking.Id;

                return new BookingResponse
                {
                    Success = true,
                    Data = request.BookingDTO
                };

            }
            catch (Exception)
            {

                return new BookingResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.COULD_NOT_STORE_DATA,
                    Message = "Couldnt Store Data"
                };
            }
        }
    }
}
