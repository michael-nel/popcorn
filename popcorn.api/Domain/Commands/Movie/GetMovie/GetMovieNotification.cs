using MediatR;

namespace Domain.Commands.Movie.GetMovie
{
    public class GetMovieNotification : INotification
    {
        public GetMovieNotification(Entities.Movie movie)
        {
            Movie = movie;
        }

        public Entities.Movie Movie { get; set; }
    }
}
