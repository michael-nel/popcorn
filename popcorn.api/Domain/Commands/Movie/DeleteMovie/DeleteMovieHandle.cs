using Domain.Interfaces.Repositories;
using Domain.Resources;
using MediatR;
using prmToolkit.NotificationPattern;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Commands.Movie.DeleteMovie
{
    public class DeleteMovieHandle : Notifiable, IRequestHandler<DeleteMovieRequest, Response>
    {
        private readonly IMediator _mediator;
        private readonly IRepositoryMovie _repositoryMovie;
        private readonly IRepositorySession _repositorySession;

        public DeleteMovieHandle(IMediator mediator, IRepositoryMovie repositoryMovie, IRepositorySession repositorySession)
        {
            _mediator = mediator;
            _repositoryMovie = repositoryMovie;
            _repositorySession = repositorySession;
        }

        public async Task<Response> Handle(DeleteMovieRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                AddNotification("Request", Resource.RequestNotbeNull);
                return new Response(this);
            }

            var movie = _repositoryMovie.GetById(request.Id);

            if (movie == null)
            {
                AddNotification("Request", Resource.MovieNotFound);
                return new Response(this);
            }

            if (_repositorySession.Exists(x => x.Movie == movie))
            {
                AddNotification("Request", Resource.MovieExistsInSession);
                return new Response(this);
            }

            _repositoryMovie.Delete(movie);

            var result = new { Id = movie.Id };

            var response = new Response(this, result);

            DeleteMovieNotification removerMovieNotification = new DeleteMovieNotification(movie);
            await _mediator.Publish(removerMovieNotification);

            return await Task.FromResult(response);
        }
    }
}
