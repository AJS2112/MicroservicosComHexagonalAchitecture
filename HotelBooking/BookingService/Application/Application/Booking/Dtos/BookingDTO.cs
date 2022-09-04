using Application.Guest.DTO;
using Application.Room.DTO;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Booking.Dtos
{
    public class BookingDTO
    {
        public int Id { get; set; }
        public DateTime PlacedAt { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int RoomId { get; set; }
        public int GuestId { get; set; }
        public Status CurrentStatus { get; set; }

        public static Domain.Entities.Booking MapToEntity(BookingDTO bookingDTO)
        {
            return new Domain.Entities.Booking
            {
             Id = bookingDTO.Id,
             PlacedAt = bookingDTO.PlacedAt,
             Start = bookingDTO.Start,
             End = bookingDTO.End,
             Guest = new Domain.Entities.Guest() { Id = bookingDTO.GuestId },
             Room = new Domain.Entities.Room() { Id = bookingDTO.RoomId }
            };
        }

        public static BookingDTO  MapToDTO(Domain.Entities.Booking booking)
        {
            return new BookingDTO
            {
                Id = booking.Id,
                PlacedAt = booking.PlacedAt,
                Start = booking.Start,
                End = booking.End,
                GuestId = booking.Guest.Id ,
                RoomId = booking.Room.Id 
            };
        }
    }
}
