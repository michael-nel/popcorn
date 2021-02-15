using Domain.Commands.Session.DeleteSession;
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
    public class DeleteSessionTest
    {
        private readonly Mock<IMediator> _mediator;
        private readonly IRepositorySession _repositorySession;
        private readonly DeleteSessionRequest _command;
        private readonly Guid _id;
        public DeleteSessionTest()
        {
            _mediator = new Mock<IMediator>();
            _id = Guid.NewGuid();
            _repositorySession = SessionRepositoryBuilder.Instance().Find(_id).Build();
            _command = new DeleteSessionRequest(_id);
        }

        [Fact]
        public async void ShouldDeleteMovie()
        {
            var repositorySession = SessionRepositoryBuilder.Instance().Find(_id).NotExists().Build();
            DeleteSessionHandle handler = new DeleteSessionHandle(_mediator.Object, repositorySession);
            var response = await handler.Handle(_command, new System.Threading.CancellationToken());
            response.Success.Should().BeTrue();
        }

        [Fact]
        public async void ShoulbeNotDeleteRequestIsNull()
        {
            DeleteSessionHandle handler = new DeleteSessionHandle(_mediator.Object, _repositorySession);
            var response = await handler.Handle(null, new System.Threading.CancellationToken());
            response.Notifications.Should().ContainSingle(e => e.Message == Resource.RequestNotbeNull);
        }

        [Fact]
        public async void ShoulbeNotDeleteRequestNotFound()
        {
            var repositorySession = SessionRepositoryBuilder.Instance().NotFound().Build();
            DeleteSessionHandle handler = new DeleteSessionHandle(_mediator.Object, repositorySession);
            var response = await handler.Handle(_command, new System.Threading.CancellationToken());
            response.Notifications.Should().ContainSingle(e => e.Message == Resource.SessionNotFound);
        }

        [Fact]
        public async void ShoulBeNotDeleteSessionUnder10Days()
        {
            var repositorySession = SessionRepositoryBuilder.Instance().Find(_id).Exists().Build();
            DeleteSessionHandle handler = new DeleteSessionHandle(_mediator.Object, repositorySession);
            var response = await handler.Handle(_command, new System.Threading.CancellationToken());
            response.Notifications.Should().ContainSingle(e => e.Message == Resource.SessionCanNotBeDeleteUnder10Days);
        }

    }
}
