using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Ports;
using Microsoft.EntityFrameworkCore;

namespace Data.Rooms
{
    public class RoomRepository : IRoomRepository
    {
        private readonly HotelDbContext _hotelDbContext;
        public RoomRepository(HotelDbContext hotelDbContext)
        {
            _hotelDbContext = hotelDbContext;
        }

        public async Task<int> Create(Room room)
        {
            _hotelDbContext.Rooms.Add(room);
            await _hotelDbContext.SaveChangesAsync();
            return room.Id;
        }

        public Task<Room> Get(int id)
        {
            return  _hotelDbContext.Rooms
                .Include(r => r.Bookings)
                .Where(r => r.Id == id).FirstOrDefaultAsync();
        }

        public Task Update(Room room)
        {
            throw new NotImplementedException();
        }
    }
}
