using Domain.Commands.Room.GetRoom;
using Infra.Repositories.Transactions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using UseCases.Interfaces;

namespace api.Controllers
{
    public class RoomController : Base.ControllerBase
    {
        private readonly IRoomUseCase _roomUseCase;

        public RoomController(IRoomUseCase roomUseCase, IUnitOfWork unitOfwork) : base(unitOfwork)
        {
            _roomUseCase = roomUseCase;
        }

        [Authorize]
        [HttpGet]
        [Route("api/room")]
        public async Task<IActionResult> GetRoom()
        {
            try
            {
                var request = new GetRoomRequest();
                var response = await _roomUseCase.Get(request);
                return await ResponseAsync(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
