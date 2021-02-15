using Domain.Entities.Base;
using Domain.Enums;
using Domain.Resources;
using prmToolkit.NotificationPattern;
using System;

namespace Domain.Entities
{
    public class Session : EntityBase
    {
        protected Session() { }
        public Session(DateTime sessionStart, DateTime sessionEnd, decimal ticketValue, AnimationType animation, AudioType audio, Guid movieId, Guid roomId)
        {
            SessionStart = sessionStart;
            SessionEnd = sessionEnd;
            TicketValue = ticketValue;
            Animation = animation;
            Audio = audio;
            MovieId = movieId;
            RoomId = roomId;

            new AddNotifications<Session>(this)
                .IfEqualsZero(x => x.TicketValue, Resource.TicketIsInvalid);
        }
        public DateTime SessionStart { get; set; }
        public DateTime SessionEnd { get; set; }
        public decimal TicketValue { get; set; }
        public AnimationType Animation { get; set; }
        public AudioType Audio { get; set; }
        public Guid MovieId { get; set; }
        public virtual Movie Movie { get; set; }
        public Guid RoomId { get; set; }
        public virtual Room Room { get; set; }

    }
}
