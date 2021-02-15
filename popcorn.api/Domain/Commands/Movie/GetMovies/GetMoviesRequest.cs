using MediatR;

namespace Domain.Commands.Movie.GetMovies
{
    public class GetMoviesRequest : IRequest<GetMoviesResponse>
    {
        public GetMoviesRequest(int pages)
        {
            Pages = pages;
        }

        public int Pages { get; set; }
    }
}
