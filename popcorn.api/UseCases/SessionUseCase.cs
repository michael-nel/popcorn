using Domain.Commands;
using Domain.Commands.Session.AddSession;
using Domain.Commands.Session.DeleteSession;
using Domain.Commands.Session.GetSessions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Interfaces;

namespace UseCases
{
    public class SessionUseCase : ISessionUseCase
    {
        private readonly IMediator _mediator;
        public SessionUseCase(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<Response> Add(AddSessionRequest addSessionRequest)
        {
            var responseSession = await _mediator.Send(addSessionRequest, CancellationToken.None);
            return responseSession;
        }
        public async Task<Response> Delete(DeleteSessionRequest deleteSessionRequest)
        {
            var responseSession = await _mediator.Send(deleteSessionRequest, CancellationToken.None);
            return responseSession;
        }
        public async Task<GetSessionsResponse> GetList(GetSessionsRequest getSessionsRequest)
        {
            var responseMovie = await _mediator.Send(getSessionsRequest, CancellationToken.None);
            return responseMovie;
        }
    }
}
