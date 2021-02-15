using MediatR;

namespace Domain.Commands.Movie.AddMovie
{
    public class AddMovieNotification : INotification
    {
        public AddMovieNotification(Entities.Movie movie)
        {
            Movie = movie;
        }
        public Entities.Movie Movie { get; set; }
    }
}
