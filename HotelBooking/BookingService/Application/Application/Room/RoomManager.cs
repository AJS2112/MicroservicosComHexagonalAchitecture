using Application.Guest.DTO;
using Application.Guest.Responses;
using Application.Room.DTO;
using Application.Room.Ports;
using Application.Room.Requests;
using Application.Room.Responses;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Room
{
    public class RoomManager : IRoomManager
    {
        private readonly IRoomRepository _roomRepository;

        public RoomManager(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;   
        }

        public async Task<RoomResponse> CreateRoom(CreateRoomRequest request)
        {
            try
            {
                var room = RoomDTO.MapToEntity(request.Data);
                await room.Save(_roomRepository);
                request.Data.Id = room.Id;

                return new RoomResponse
                {
                    Data = request.Data,
                    Success = true
                };
            }
            catch (MissingRequiredInformationException)
            {
                return new RoomResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.MISSING_REQUIRED_INFORMATION,
                    Message = "There is missing informartion"
                };
            }
        }

        public async Task<RoomResponse> GetRoom(int roomId)
        {
            var room = await _roomRepository.Get(roomId);
            if (room != null)
            {
                return new RoomResponse
                {
                    Data = RoomDTO.MapToDTO(room),
                    Success = true
                };

            }
            return new RoomResponse
            {
                Success = false,
                ErrorCode = ErrorCodes.GUEST_NOT_FOUND,
                Message = "No Room Record Founded"
            };
        }

    }
}
