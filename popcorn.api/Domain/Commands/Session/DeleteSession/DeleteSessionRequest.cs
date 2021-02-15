using MediatR;
using System;

namespace Domain.Commands.Session.DeleteSession
{
    public class DeleteSessionRequest : IRequest<Response>
    {
        public DeleteSessionRequest(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
