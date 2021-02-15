using Domain.Commands;
using Domain.Commands.Room.GetRoom;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Interfaces;

namespace UseCases
{
    public class RoomUseCase : IRoomUseCase
    {
        private readonly IMediator _mediator;
        public RoomUseCase(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<Response> Get(GetRoomRequest getRoomRequest)
        {
            var getRoom = await _mediator.Send(getRoomRequest, CancellationToken.None);
            return getRoom;
        }
    }
}
