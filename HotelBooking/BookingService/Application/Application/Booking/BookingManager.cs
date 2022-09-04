using Application.Booking.Dtos;
using Application.Booking.Ports;
using Application.Booking.Responses;
using Application.Payment.Ports;
using Domain.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Booking
{

    public class BookingManager : IBookingManager
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IGuestRepository _guestRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IPaymentProcessorFactory _paymentProcessorFactory;


        public BookingManager(
            IBookingRepository bookingRepository,
            IGuestRepository guestRepository,
            IRoomRepository roomRepository,
            IPaymentProcessorFactory paymentProcessorFactory
            )
        {
            _bookingRepository = bookingRepository;
            _guestRepository = guestRepository;
            _roomRepository = roomRepository;
            _paymentProcessorFactory = paymentProcessorFactory; 
        }

        public async Task<BookingResponse> CreateBooking(BookingDTO bookingDto)
        {
            try
            {
                var booking = BookingDTO.MapToEntity(bookingDto);
                booking.Guest = await _guestRepository.Get(bookingDto.GuestId);
                booking.Room = await _roomRepository.Get(bookingDto.RoomId);

                await _bookingRepository.Create(booking);
                bookingDto.Id = booking.Id;

                return new BookingResponse
                {
                    Success = true,
                    Data = bookingDto
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

        public Task<BookingDTO> GetBooking(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<string> PayForABooking(PaymentRequestDTO paymentRequestDTO)
        {
            var paymentProcessor = _paymentProcessorFactory.GetPaymentProcessor(paymentRequestDTO.PaymentProvider);

            var response = await paymentProcessor.CapturePayment(paymentRequestDTO);

            return response.ToString();
        }

        Task<BookingDTO> IBookingManager.CreateBooking(BookingDTO bookingDto)
        {
            throw new NotImplementedException();
        }
    }
}
