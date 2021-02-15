using Domain.Interfaces.Repositories;
using Domain.Resources;
using MediatR;
using prmToolkit.NotificationPattern;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Commands.Session.AddSession
{
    public class AddSessionHandle : Notifiable, IRequestHandler<AddSessionRequest, Response>
    {
        private readonly IMediator _mediator;
        private readonly IRepositorySession _repositorySession;
        public AddSessionHandle(IMediator mediator, IRepositorySession repositorySession)
        {
            _mediator = mediator;
            _repositorySession = repositorySession;
        }

        public async Task<Response> Handle(AddSessionRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                AddNotification("Request", Resource.RequestNotbeNull);
                return new Response(this);
            }
            
            if(string.IsNullOrEmpty(request.SessionEnd))
            {
                AddNotification("Request", Resource.SessionEndIsInvalid);
                return new Response(this);
            }

            
            var time = TimeSpan.Parse(request.SessionEnd);
            var sessionEnd = request.SessionStart;
            sessionEnd = sessionEnd.AddHours(time.Hours);
            sessionEnd = sessionEnd.AddMinutes(time.Minutes);

            Entities.Session session = new Entities.Session(request.SessionStart, sessionEnd, request.TicketValue, request.Animation, request.Audio, request.MovieId, request.RoomId);
            AddNotifications(session);

            if (IsInvalid())
            {
                return new Response(this);
            }

            if (_repositorySession.Exists( x => x.RoomId == session.RoomId && 
                    (
                        (session.SessionStart >= x.SessionStart  && session.SessionStart <= x.SessionEnd) ||
                        (session.SessionEnd >= x.SessionStart && session.SessionEnd <= x.SessionEnd)
                    )
                )
              )
            {
                AddNotification("Request", Resource.ExisteRoominAnotherSession);
                return new Response(this);
            }

            session = _repositorySession.Add(session);
            AddSessionNotification addSessionNotification = new AddSessionNotification(session);
            var response = new Response(this, session);

            await _mediator.Publish(addSessionNotification);
            return await Task.FromResult(response);
        }

    }
}
