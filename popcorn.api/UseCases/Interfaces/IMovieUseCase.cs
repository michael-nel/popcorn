using Domain.Commands;
using Domain.Commands.Movie.AddMovie;
using Domain.Commands.Movie.DeleteMovie;
using Domain.Commands.Movie.GetMovie;
using Domain.Commands.Movie.GetMovies;
using Domain.Commands.Movie.UpdateMovie;
using System.Threading.Tasks;

namespace UseCases.Interfaces
{
    public interface IMovieUseCase
    {
        Task<Response> Add(AddMovieRequest addMovieRequest);
        Task<Response> Delete(DeleteMovieRequest addMovieRequest);
        Task<Response> Get(GetMovieRequest getMovieRequest);
        Task<GetMoviesResponse> GetList(GetMoviesRequest getMovieRequest);
        Task<Response> Update(UpdateMovieRequest updateMovieRequest);
    }
}