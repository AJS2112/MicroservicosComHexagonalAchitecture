using Application.Guest.DTO;
using Domain.Enums;
using Entities = Domain.Entities;


namespace Application.Room.DTO
{
    public class RoomDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public bool InMaintenance { get; set; }
        public decimal Value { get; set; }
        public int Currency { get; set; }

        public static Entities.Room MapToEntity(RoomDTO roomDTO)
        {

            return new Entities.Room
            {
                Id = roomDTO.Id,
                Name = roomDTO.Name,
                Level = roomDTO.Level,
                InMaintenance = roomDTO.InMaintenance,
                Price = new Domain.ValueObjects.Price
                {
                    Value = roomDTO.Value,
                    Currency = (AcceptedCurrencies) roomDTO.Currency
                }
            };
        }

        public static RoomDTO MapToDTO(Entities.Room room)
        {
            return new RoomDTO
            {
                Id = room.Id,
                Name = room.Name,
                Level = room.Level,
                InMaintenance = room.InMaintenance,
                Value = room.Price.Value,
                Currency = (int)room.Price.Currency
            };
        }
    }
}
