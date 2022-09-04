using Domain.Ports;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Booking
{
    public class BookingRepository : IBookingRepository
    {
        private readonly HotelDbContext _hotelDbContext;

        public BookingRepository(HotelDbContext hotelDbContext)
        {
            _hotelDbContext = hotelDbContext;
        }
    
        public async Task<Domain.Entities.Booking> Create(Domain.Entities.Booking booking)
        {
            _hotelDbContext.Bookings.Add(booking);
            await _hotelDbContext.SaveChangesAsync();
            return booking;
        }

        public async Task<Domain.Entities.Booking> Get(int id)
        {
            return await _hotelDbContext.Bookings.Where(b => b.Id == id).FirstOrDefaultAsync();
        }

        public Task Update(Domain.Entities.Booking booking)
        {
            throw new NotImplementedException();
        }
    }
}
