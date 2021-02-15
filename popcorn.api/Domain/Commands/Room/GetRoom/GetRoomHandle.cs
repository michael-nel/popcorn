using Domain.Interfaces.Repositories;
using Domain.Resources;
using MediatR;
using prmToolkit.NotificationPattern;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Commands.Room.GetRoom
{
    public class GetRoomHandle : Notifiable, IRequestHandler<GetRoomRequest, Response>
    {
        private readonly IMediator _mediator;
        private readonly IRepositoryRoom _repositoryRoom;

        public GetRoomHandle(IMediator mediator, IRepositoryRoom repositoryRoom)
        {
            _mediator = mediator;
            _repositoryRoom = repositoryRoom;
        }

        public async Task<Response> Handle(GetRoomRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                AddNotification("Request", Resource.RequestNotbeNull);
                return new Response(this);
            }

            var roomCollection = _repositoryRoom.GetOrderFor(x=> x.Name).ToList();

            var response = new Response(this, roomCollection);

            return await Task.FromResult(response);
        }
    }
}
