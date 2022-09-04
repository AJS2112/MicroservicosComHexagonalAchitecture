using Microsoft.AspNetCore.Mvc;
using Application.Room.Ports;
using Application.Room;
using Application.Room.DTO;
using Application.Room.Requests;
using Application.Guest.DTO;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly ILogger<RoomController> _logger;
        private readonly IRoomManager _roomManager;
        public RoomController(ILogger<RoomController> logger, IRoomManager roomManager)
        {
            _logger = logger;
            _roomManager = roomManager;
        }
    
        [HttpPost]
        public async Task<ActionResult<RoomDTO>> Post(RoomDTO roomDTO)
        {
            var request = new CreateRoomRequest()
            {
                Data = roomDTO
            };

            var res = await _roomManager.CreateRoom(request);

            if (res.Success)
            {
                return Created("", res.Data);
            }

            else if (res.ErrorCode == Application.ErrorCodes.MISSING_REQUIRED_INFORMATION)
            {
                return BadRequest(res);
            }

            _logger.LogError("Response with error code", res);
            return BadRequest(500);
        }

        [HttpGet]
        public async Task<ActionResult<RoomDTO>> Get(int id)
        {
            var res = await _roomManager.GetRoom(id);

            if (res.Success) return Ok(res.Data);

            if (res.ErrorCode > 0) return BadRequest(res);

            _logger.LogError("Response with unknow Error Code Returned", res);

            return BadRequest(500);

        }

    }
}
