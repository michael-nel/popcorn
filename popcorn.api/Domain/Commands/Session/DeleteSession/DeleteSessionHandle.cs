using Domain.Interfaces.Repositories;
using Domain.Resources;
using MediatR;
using prmToolkit.NotificationPattern;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Commands.Session.DeleteSession
{
    public class DeleteSessionHandle : Notifiable, IRequestHandler<DeleteSessionRequest, Response>
    {
        private readonly IMediator _mediator;
        private readonly IRepositorySession _repositorySession;

        public DeleteSessionHandle(IMediator mediator, IRepositorySession repositorySession)
        {
            _mediator = mediator;
            _repositorySession = repositorySession;
        }

        public async Task<Response> Handle(DeleteSessionRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                AddNotification("Request", Resource.RequestNotbeNull);
                return new Response(this);
            }

            Entities.Session session = _repositorySession.GetById(request.Id);

            if (session == null)
            {
                AddNotification("Request", Resource.SessionNotFound);
                return new Response(this);
            }

            var dateNow = DateTime.Now;
            dateNow.AddDays(10);

            if(_repositorySession.Exists(x=> x.Id == request.Id && x.SessionStart <= dateNow))
            {
                AddNotification("Request", Resource.SessionCanNotBeDeleteUnder10Days);
                return new Response(this);
            }

            _repositorySession.Delete(session);

            var result = new { Id = session.Id };

            var response = new Response(this, result);

            DeleteSessionNotification removeSessionNotification = new DeleteSessionNotification(session);
            await _mediator.Publish(removeSessionNotification);

            return await Task.FromResult(response);
        }
    }
}
