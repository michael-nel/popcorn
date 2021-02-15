using Domain.Interfaces.Repositories;
using Domain.Resources;
using MediatR;
using prmToolkit.NotificationPattern;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Commands.Movie.UpdateMovie
{
    public class UpdateMovieHandle : Notifiable, IRequestHandler<UpdateMovieRequest, Response>
    {
        private readonly IMediator _mediator;
        private readonly IRepositoryMovie _repositoryMovie;

        public UpdateMovieHandle(IMediator mediator, IRepositoryMovie repositoryMovie)
        {
            _mediator = mediator;
            _repositoryMovie = repositoryMovie;
        }

        public async Task<Response> Handle(UpdateMovieRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                AddNotification("Resquest", Resource.RequestNotbeNull);
                return new Response(this);
            }

            if (string.IsNullOrEmpty(request.Duration) || TimeSpan.Zero == TimeSpan.Parse(request.Duration))
            {
                AddNotification("Duração", Resource.DurationIsInvalid);
                return new Response(this);
            }

            var movie = _repositoryMovie.GetById(request.Id);


            if (movie == null)
            {
                AddNotification("Filme", Resource.MovieNotFound);
                return new Response(this);
            }

            movie.Update(request.Image, request.Title, request.Description, TimeSpan.Parse(request.Duration));

            movie = _repositoryMovie.Update(movie);

            var response = new Response(this, movie);

            return await Task.FromResult(response);
        }

    }
}
