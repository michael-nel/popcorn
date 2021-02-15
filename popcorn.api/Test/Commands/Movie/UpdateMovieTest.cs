using Bogus;
using Domain.Commands.Movie.UpdateMovie;
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
    public class UpdateMovieTest
    {
        private readonly Mock<IMediator> _mediator;
        private readonly IRepositoryMovie _repositoryMovie;
        private readonly UpdateMovieRequest _command;
        private readonly Faker _faker;
        private readonly Guid _id;

        public UpdateMovieTest()
        {
            _mediator = new Mock<IMediator>();
            _faker = new Faker();
            _id = Guid.NewGuid();
            _repositoryMovie = MovieRepositoryBuilder.Instance().Find(_id).Build();
            _command = new UpdateMovieRequest
            {
                Id = _id,
                Image = _faker.Image.ToString(),
                Title = "Title",
                Description = _faker.Lorem.Paragraph(5),
                Duration = "01:00"
            };
        }

        [Fact]
        public async void ShoulbeUpdate()
        {
            UpdateMovieHandle handler = new UpdateMovieHandle(_mediator.Object, _repositoryMovie);
            var response = await handler.Handle(_command, new System.Threading.CancellationToken());
            response.Success.Should().BeTrue();
        }

        [Fact]
        public async void ShoulbeNotUpdateRequestIsNull()
        {
            UpdateMovieHandle handler = new UpdateMovieHandle(_mediator.Object, _repositoryMovie);
            var response = await handler.Handle(null, new System.Threading.CancellationToken());
            response.Notifications.Should().ContainSingle(e => e.Message == Resource.RequestNotbeNull);
        }

        [Fact]
        public async void ShoulBeNotFound()
        {
            var repositoryMovie = MovieRepositoryBuilder.Instance().NotFound().Build();
            UpdateMovieHandle handler = new UpdateMovieHandle(_mediator.Object, repositoryMovie);
            var response = await handler.Handle(_command, new System.Threading.CancellationToken());
            response.Notifications.Should().ContainSingle(e => e.Message == Resource.MovieNotFound);
        }
    }
}
