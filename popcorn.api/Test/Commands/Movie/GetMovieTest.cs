using Domain.Commands.Movie.GetMovie;
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
    public class GetMovieTest
    {
        private readonly Mock<IMediator> _mediator;
        private readonly IRepositoryMovie _repositoryMovie;
        private readonly GetMovieRequest _command;
        private readonly Guid _id;

        public GetMovieTest()
        {
            _mediator = new Mock<IMediator>();
            _id = Guid.NewGuid();
            _repositoryMovie = MovieRepositoryBuilder.Instance().Find(_id).Build();
            _command = new GetMovieRequest(_id);
        }


        [Fact]
        public async void ShoulBeFind()
        {
            GetMovieHandle handler = new GetMovieHandle(_mediator.Object, _repositoryMovie);
            var response = await handler.Handle(_command, new System.Threading.CancellationToken());
            response.Success.Should().BeTrue();
        }

        [Fact]
        public async void ShoulbeNotCreateRequestIsNull()
        {
            GetMovieHandle handler = new GetMovieHandle(_mediator.Object, _repositoryMovie);
            var response = await handler.Handle(null, new System.Threading.CancellationToken());
            response.Notifications.Should().ContainSingle(e => e.Message == Resource.RequestNotbeNull);
        }

        [Fact]
        public async void ShoulBeNotFound()
        {
            var repositoryMovie = MovieRepositoryBuilder.Instance().NotFound().Build();
            GetMovieHandle handler = new GetMovieHandle(_mediator.Object, repositoryMovie);
            var response = await handler.Handle(_command, new System.Threading.CancellationToken());
            response.Notifications.Should().ContainSingle(e => e.Message == Resource.MovieNotFound);
        }
    }
}
