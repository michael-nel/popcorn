using Domain.Interfaces.Repositories;
using Domain.Resources;
using MediatR;
using prmToolkit.NotificationPattern;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Commands.Movie.GetMovies
{
    public class GetMoviesHandle : Notifiable, IRequestHandler<GetMoviesRequest, GetMoviesResponse>
    {
        private readonly IMediator _mediator;
        private readonly IRepositoryMovie _repositoryMovie;

        public GetMoviesHandle(IMediator mediator, IRepositoryMovie repositoryMovie)
        {
            _mediator = mediator;
            _repositoryMovie = repositoryMovie;
        }

        public async Task<GetMoviesResponse> Handle(GetMoviesRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                AddNotification("Request", Resource.RequestNotbeNull);
                return null;
            }
            var response = new GetMoviesResponse();

            if (request.Pages == -1)
            {
                var roomCollection = _repositoryMovie.GetOrderFor(x => x.Title).ToList();
                response.Movies = roomCollection;
                response.TotalPages = 1;
                response.Page = 1;
            }
            else
            {
                var skipPages = request.Pages * 10;
                var total = ((decimal)_repositoryMovie.Get().Count()) / 10;
                var totalPages = (int)Math.Ceiling(total) + 1;

                var roomCollection = _repositoryMovie.GetOrderFor(x => x.Title).Skip(skipPages).Take(10).ToList();
                response.Movies = roomCollection;
                response.TotalPages = totalPages;
                response.Page = request.Pages;
            }
            return await Task.FromResult(response);

        }
    }
}
