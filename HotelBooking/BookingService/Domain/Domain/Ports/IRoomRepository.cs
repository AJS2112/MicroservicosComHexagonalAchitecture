using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports
{
    public interface IRoomRepository
    {
        Task<Room> Get(int id);
        Task<int> Create(Room room);
        Task Update(Room room);
    }
}
