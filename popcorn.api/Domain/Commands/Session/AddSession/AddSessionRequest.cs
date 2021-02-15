using Domain.Enums;
using MediatR;
using System;

namespace Domain.Commands.Session.AddSession
{
    public class AddSessionRequest : IRequest<Response>
    {
        public DateTime SessionStart { get; set; }
        public string SessionEnd { get; set; }
        public decimal TicketValue { get; set; }
        public AnimationType Animation { get; set; }
        public AudioType Audio { get; set; }
        public Guid MovieId { get; set; }
        public Guid RoomId { get; set; }
    }
}
