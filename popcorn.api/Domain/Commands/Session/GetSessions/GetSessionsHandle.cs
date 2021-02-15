using Domain.Interfaces.Repositories;
using Domain.Resources;
using MediatR;
using prmToolkit.NotificationPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Commands.Session.GetSessions
{
    public class GetSessionsHandle : Notifiable, IRequestHandler<GetSessionsRequest, GetSessionsResponse>
    {
        private readonly IMediator _mediator;
        private readonly IRepositorySession _repositorySession;

        public GetSessionsHandle(IMediator mediator, IRepositorySession repositorySession)
        {
            _mediator = mediator;
            _repositorySession = repositorySession;
        }

        public async Task<GetSessionsResponse> Handle(GetSessionsRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                AddNotification("Request", Resource.RequestNotbeNull);
                return null;
            }

            var skipPages = request.Pages * 10;
            var total = ((decimal)_repositorySession.Get().Count()) / 10;
            var totalPages = (int)Math.Round(total) + 1;

            var sessionCollection = _repositorySession.GetOrderFor(x => x.SessionStart, false, x => x.Movie, x => x.Room).Skip(skipPages).Take(10).ToList();

            var response = new GetSessionsResponse
            {
                Sessions = sessionCollection,
                TotalPages = totalPages,
                Page = request.Pages
            };

            return await Task.FromResult(response);
        }
    }
}
