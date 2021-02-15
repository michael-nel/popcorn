using Domain.Interfaces.Repositories;
using Domain.Resources;
using MediatR;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Commands.Movie.GetMovie
{
    public class GetMovieHandle : Notifiable, IRequestHandler<GetMovieRequest, Response>
    {
        private readonly IMediator _mediator;
        private readonly IRepositoryMovie _repositoryMovie;

        public GetMovieHandle(IMediator mediator, IRepositoryMovie repositoryMovie)
        {
            _mediator = mediator;
            _repositoryMovie = repositoryMovie;
        }

        public async Task<Response> Handle(GetMovieRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                AddNotification("Request", Resource.RequestNotbeNull);
                return new Response(this);
            }

            Entities.Movie movie = _repositoryMovie.GetById(request.Id);

            if (movie == null)
            {
                AddNotification("Request", Resource.MovieNotFound);
                return new Response(this);
            }

            var response = new Response(this, movie);

            return await Task.FromResult(response);
        }
    }
}
