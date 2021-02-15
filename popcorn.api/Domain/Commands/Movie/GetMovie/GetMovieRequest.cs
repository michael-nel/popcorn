using MediatR;
using System;

namespace Domain.Commands.Movie.GetMovie
{
    public class GetMovieRequest : IRequest<Response>
    {
        public GetMovieRequest(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
