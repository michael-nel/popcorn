using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Commands.Movie.UpdateMovie
{
    public class UpdateMovieRequest : IRequest<Response>
    {
        public Guid Id { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Duration { get; set; }
    }
}
