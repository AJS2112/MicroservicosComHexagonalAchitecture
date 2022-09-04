using Domain.Exceptions;
using Domain.Ports;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public bool InMaintenance { get; set; }
        public Price Price { get; set; }
        public ICollection<Booking> Bookings { get; set; }  

        public bool IsAvailable
        {
            get
            {
                if (this.InMaintenance || this.HasGuest)
                {
                    return false;
                }
                return true;
            }
        }
        public bool HasGuest
        {
            get {
                var notAvailableStatus = new List<Enums.Status>()
                {
                    Enums.Status.Created,
                    Enums.Status.Paid
                };

                return this.Bookings.Where(
                    b => b.Room.Id == this.Id &&
                    notAvailableStatus.Contains(b.CurrentStatus)
                    ).Count() > 0;
                    
            }
        }

        public void ValidateState()
        {
            if (string.IsNullOrEmpty(Name) )
            {
                throw new MissingRequiredInformationException();
            }

        }

        public async Task Save(IRoomRepository roomRepository)
        {
            this.ValidateState();

            if (this.Id == 0)
            {
                this.Id = await roomRepository.Create(this);
            }
            else
            {
                await roomRepository.Update(this);
            }
        }
    }
}
