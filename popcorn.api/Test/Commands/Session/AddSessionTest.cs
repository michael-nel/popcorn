using Bogus;
using Domain.Commands.Session.AddSession;
using Domain.Enums;
using Domain.Interfaces.Repositories;
using Domain.Resources;
using FluentAssertions;
using MediatR;
using Moq;
using System;
using Test.Repositories;
using Xunit;

namespace Test.Commands.Session
{
    public class AddSessionTest
    {
        private readonly Faker _faker;
        private readonly Mock<IMediator> _mediator;
        private readonly IRepositorySession _repositorySession;
        private readonly AddSessionRequest _command;
        private readonly Guid _id;

        public AddSessionTest()
        {
            _faker = new Faker();
            _mediator = new Mock<IMediator>();
            _id = Guid.NewGuid();
            _repositorySession = SessionRepositoryBuilder.Instance().NotExists().Build();
            _command = new AddSessionRequest
            {
                SessionStart = _faker.Date.Recent(1),
                SessionEnd = "01:30",
                TicketValue = _faker.Finance.Amount(1, 200),
                Animation = _faker.PickRandom<AnimationType>(),
                Audio = _faker.PickRandom<AudioType>(),
                RoomId = Guid.NewGuid(),
                MovieId = Guid.NewGuid()
            }
            ;
        }
        
        [Fact]
        public async void ShouldBeCreate()
        {
            AddSessionHandle handler = new AddSessionHandle(_mediator.Object, _repositorySession);
            var response = await handler.Handle(_command, new System.Threading.CancellationToken());
            response.Success.Should().BeTrue();
        }

        [Fact]
        public async void ShoulbeNotCreateRequestIsNull()
        {
            AddSessionHandle handler = new AddSessionHandle(_mediator.Object, _repositorySession);
            var response = await handler.Handle(null, new System.Threading.CancellationToken());
            response.Notifications.Should().ContainSingle(e => e.Message == Resource.RequestNotbeNull);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]

        public async void ShoulbeNotCreateSessionEndInvalid(string sessionEnd)
        {
            _command.SessionEnd = sessionEnd;
            AddSessionHandle handler = new AddSessionHandle(_mediator.Object, _repositorySession);
            var response = await handler.Handle(_command, new System.Threading.CancellationToken());
            response.Notifications.Should().ContainSingle(e => e.Message == Resource.SessionEndIsInvalid);
        }

        [Fact]
        public async void ShoulbeNotCreateExistAnotherSession()
        {
            var repositorySession = SessionRepositoryBuilder.Instance().Exists().Build();
            AddSessionHandle handler = new AddSessionHandle(_mediator.Object, repositorySession);
            var response = await handler.Handle(_command, new System.Threading.CancellationToken());
            response.Notifications.Should().ContainSingle(e => e.Message == Resource.ExisteRoominAnotherSession);
        }

    }
}
