using MediatR;
using System;

namespace Domain.Commands.Movie.AddMovie
{
    public class AddMovieRequest : IRequest<Response>
    {
        public string Image { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Duration { get; set; }
    }
}
