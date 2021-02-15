using MediatR;
using System;

namespace Domain.Commands.Movie.DeleteMovie
{
    public class DeleteMovieRequest : IRequest<Response>
    {
        public DeleteMovieRequest(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
