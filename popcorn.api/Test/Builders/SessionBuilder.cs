using Bogus;
using Domain.Entities;
using Domain.Enums;
using System;

namespace Test.Builders
{
    public class SessionBuilder
    {
     
        protected Guid Id { get; set; }
        protected DateTime SessionStart { get; set; }
        protected DateTime SessionEnd { get; set; }
        protected decimal TicketValue { get; set; }
        protected AnimationType Animation { get; set; }
        protected AudioType Audio { get; set; }
        protected Guid MovieId { get; set; }
        protected virtual Movie Movie { get; set; }
        protected Guid RoomId { get; set; }
        protected virtual Room Room { get; set; }
        public static SessionBuilder New()
        {
            var faker = new Faker();

            var room = RoomBuilder.New().WithId(Guid.NewGuid()).Build();

            var movie = MovieBuilder.New().WithId(Guid.NewGuid()).Build();

            return new SessionBuilder
            {
                SessionStart = faker.Date.Recent(1),
                SessionEnd = faker.Date.Recent(1),
                TicketValue = faker.Finance.Amount(1, 200),
                Animation = faker.PickRandom<AnimationType>(),
                Audio = faker.PickRandom<AudioType>(),
                Room = room,
                Movie = movie,
                RoomId = room.Id,
                MovieId = movie.Id
            };
        }
        public Session Build()
        {
            var session = new Session(SessionStart, SessionEnd, TicketValue, Animation, Audio, MovieId, RoomId);
            if (Id == null) return session;
            var propertyInfo = session.GetType().GetProperty("Id");
            propertyInfo.SetValue(session, Convert.ChangeType(Id, propertyInfo.PropertyType), null);
            return session;
        }

        public SessionBuilder WithId(Guid id)
        {
            Id = id;
            return this;
        }
        public SessionBuilder WithSessionStart(DateTime sessionStart)
        {
            SessionStart = sessionStart;
            return this;
        }
        public SessionBuilder WithSessionEnd(DateTime sessionEnd)
        {
            SessionEnd = sessionEnd;
            return this;
        }
        public SessionBuilder WithTicketValue(decimal ticketValue)
        {
            TicketValue = ticketValue;
            return this;
        }
        public SessionBuilder WithAnimation(AnimationType animation)
        {
            Animation = animation;
            return this;
        }
        public SessionBuilder WithAudio(AudioType audio)
        {
            Audio = audio;
            return this;
        }
        public SessionBuilder WithRoom(Room room)
        {
            Room = room;
            RoomId = room.Id;
            return this;
        }
        public SessionBuilder WithMovie(Movie movie)
        {
            Movie = movie;
            MovieId = movie.Id;
            return this;
        }
    }
}
