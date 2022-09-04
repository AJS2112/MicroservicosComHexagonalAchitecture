using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports
{
    public interface IBookingRepository
    {
        Task<Booking> Get(int id);
        Task<Booking> Create(Booking booking);
        Task Update(Booking booking);
    }
}
