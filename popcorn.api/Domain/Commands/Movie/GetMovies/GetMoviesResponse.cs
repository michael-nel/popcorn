using System.Collections.Generic;

namespace Domain.Commands.Movie.GetMovies
{
    public class GetMoviesResponse
    {
        public IEnumerable<Entities.Movie> Movies { get; set; }
        public int Page { get; set; }
        public int TotalPages { get; set; }
    }
}
