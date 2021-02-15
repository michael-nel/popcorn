using Bogus;
using Domain.Commands.Movie.DeleteMovie;
using Domain.Interfaces.Repositories;
using Domain.Resources;
using FluentAssertions;
using MediatR;
using Moq;
using System;
using Test.Repositories;
using Xunit;

namespace Test.Commands
{
    public class DeleteMovieTest
    {
        private readonly Faker _faker;
        private readonly Mock<IMediator> _mediator;
        private readonly IRepositoryMovie _repositoryMovie;
        private readonly IRepositorySession _repositorySession;
        private readonly DeleteMovieRequest _command;
        private readonly Guid _id;

        public DeleteMovieTest()
        {
            _faker = new Faker();
            _mediator = new Mock<IMediator>();
            _id = Guid.NewGuid();
            _repositoryMovie = MovieRepositoryBuilder.Instance().Find(_id).Build();
            _repositorySession = SessionRepositoryBuilder.Instance().Build();
            
            _command = new DeleteMovieRequest(_id);
        }

        [Fact]
        public async void ShouldDeleteMovie()
        {
            var repositorySession = SessionRepositoryBuilder.Instance().NotExists().Build();
            DeleteMovieHandle handler = new DeleteMovieHandle(_mediator.Object, _repositoryMovie, repositorySession);
            var response = await handler.Handle(_command, new System.Threading.CancellationToken());
            response.Success.Should().BeTrue();
        }

        [Fact]
        public async void ShoulbeNotDeleteRequestIsNull()
        {
            DeleteMovieHandle handler = new DeleteMovieHandle(_mediator.Object, _repositoryMovie, _repositorySession);
            var response = await handler.Handle(null, new System.Threading.CancellationToken());
            response.Notifications.Should().ContainSingle(e => e.Message == Resource.RequestNotbeNull);
        }

        [Fact]
        public async void ShoulbeDeleteRequestNotFound()
        {
            var repositoryMovie = MovieRepositoryBuilder.Instance().NotFound().Build();
            DeleteMovieHandle handler = new DeleteMovieHandle(_mediator.Object, repositoryMovie, _repositorySession);
            var response = await handler.Handle(_command, new System.Threading.CancellationToken());
            response.Notifications.Should().ContainSingle(e => e.Message == Resource.MovieNotFound);
        }

        [Fact]
        public async void ShoulbeDeleteRequestExistInstance()
        {
            var repositorySession = SessionRepositoryBuilder.Instance().Exists().Build();
            DeleteMovieHandle handler = new DeleteMovieHandle(_mediator.Object, _repositoryMovie, repositorySession);
            var response = await handler.Handle(_command, new System.Threading.CancellationToken());
            response.Notifications.Should().ContainSingle(e => e.Message == Resource.MovieExistsInSession);
        }
    }
}
