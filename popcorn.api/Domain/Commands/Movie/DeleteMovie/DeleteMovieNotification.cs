using MediatR;

namespace Domain.Commands.Movie.DeleteMovie
{
    public class DeleteMovieNotification : INotification
    {
        public DeleteMovieNotification(Entities.Movie movie)
        {
            Movie = movie;
        }

        public Entities.Movie Movie { get; set; }
    }
}
