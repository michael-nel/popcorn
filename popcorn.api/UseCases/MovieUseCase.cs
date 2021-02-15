using Domain.Commands;
using Domain.Commands.Movie.AddMovie;
using Domain.Commands.Movie.DeleteMovie;
using Domain.Commands.Movie.GetMovie;
using Domain.Commands.Movie.GetMovies;
using Domain.Commands.Movie.UpdateMovie;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Interfaces;

namespace UseCases
{
    public class MovieUseCase : IMovieUseCase
    {
        private readonly IMediator _mediator;
        public MovieUseCase(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<Response> Add(AddMovieRequest addMovieRequest)
        {
            var responseMovie =  await _mediator.Send(addMovieRequest, CancellationToken.None);
            return responseMovie;
        }
        public async Task<Response> Delete(DeleteMovieRequest deleteMovieRequest)
        {
            var responseMovie = await _mediator.Send(deleteMovieRequest, CancellationToken.None);
            return responseMovie;
        }
        public async Task<Response> Get(GetMovieRequest getMovieRequest)
        {
            var responseMovie = await _mediator.Send(getMovieRequest, CancellationToken.None);
            return responseMovie;
        }
        public async Task<GetMoviesResponse> GetList(GetMoviesRequest getMoviesRequest)
        {
            var responseMovie = await _mediator.Send(getMoviesRequest, CancellationToken.None);
            return responseMovie;
        }
        public async Task<Response> Update(UpdateMovieRequest updateMovieRequest)
        {
            var responseMovie = await _mediator.Send(updateMovieRequest, CancellationToken.None);
            return responseMovie;
        }
    }
}
