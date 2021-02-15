using Bogus;
using Domain.Entities;
using Domain.Enums;
using Domain.Resources;
using ExpectedObjects;
using FluentAssertions;
using System;
using Test.Builders;
using Xunit;

namespace Test.Entities
{
    public class SessionTest
    {
        private readonly Faker _faker;
        public SessionTest()
        {
            _faker = new Faker();
        }

        [Fact]
        public void ShouldBeCreateSession()
        {
            var room = RoomBuilder.New().WithId(Guid.NewGuid()).Build();
            var movie = MovieBuilder.New().WithId(Guid.NewGuid()).Build();

            var sessionShould = new
            {
                SessionStart = _faker.Date.Recent(1),
                SessionEnd = _faker.Date.Recent(1),
                TicketValue = _faker.Finance.Amount(1, 200),
                Animation = _faker.PickRandom<AnimationType>(),
                Audio = _faker.PickRandom<AudioType>(),
                RoomId = room.Id,
                MovieId = movie.Id
            };

            var session = new Session(sessionShould.SessionStart, sessionShould.SessionEnd,
                sessionShould.TicketValue, sessionShould.Animation, sessionShould.Audio, sessionShould.MovieId, sessionShould.RoomId);

            sessionShould.ToExpectedObject().ShouldMatch(session);
        }
        
        [Theory]
        [InlineData(0)]
        public void ShoulBeNotCreateInvalidTicketValue(decimal ticket)
        {
            var session = SessionBuilder.New().WithTicketValue(ticket).Build();
            session.Notifications.Should().ContainSingle(e => e.Message == Resource.TicketIsInvalid);
        }
    }
}
