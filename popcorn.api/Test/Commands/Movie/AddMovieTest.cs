using Bogus;
using Domain.Commands;
using Domain.Commands.Movie.AddMovie;
using Domain.Interfaces.Repositories;
using Domain.Resources;
using FluentAssertions;
using MediatR;
using Moq;
using Test.Repositories;
using Xunit;

namespace Test.Commands
{
    public class AddMovieTest
    {
        private readonly Faker _faker;
        private readonly Mock<IMediator> _mediator;
        private readonly IRepositoryMovie _repositoryMovie;
        private readonly AddMovieRequest _command;
        public AddMovieTest()
        {
            _faker = new Faker();
            _mediator = new Mock<IMediator>();
            _repositoryMovie = MovieRepositoryBuilder.Instance().Build();

            _command = new AddMovieRequest();
            _command.Image = _faker.Image.ToString();
            _command.Title = "Title";
            _command.Description = _faker.Lorem.Paragraph(5);
            _command.Duration = "01:00";
        }

        [Fact]
        public async void ShoulbeCreate()
        {
            AddMovieHandle handler = new AddMovieHandle(_mediator.Object, _repositoryMovie);
            var response = await handler.Handle(_command, new System.Threading.CancellationToken());
            response.Success.Should().BeTrue();
        }

        [Fact]
        public async void ShoulbeNotCreateRequestIsNull()
        {
            AddMovieHandle handler = new AddMovieHandle(_mediator.Object, _repositoryMovie);
            var response = await handler.Handle(null, new System.Threading.CancellationToken());
            response.Notifications.Should().ContainSingle(e => e.Message == Resource.RequestNotbeNull);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async void ShouldBeNotCreateWithImage(string image)
        {
            _command.Image = image;
            AddMovieHandle handler = new AddMovieHandle(_mediator.Object, _repositoryMovie);
            var response = await handler.Handle(_command, new System.Threading.CancellationToken());
            response.Notifications.Should().ContainSingle(e => e.Message == Resource.ImageIsRequired);
        }

        [Theory]
        [InlineData("")]
        [InlineData("00:00")]
        [InlineData(null)]
        public async void ShouldBeNotCreateWithDuration(string duration)
        {
            _command.Duration = duration;
            AddMovieHandle handler = new AddMovieHandle(_mediator.Object, _repositoryMovie);
            var response = await handler.Handle(_command, new System.Threading.CancellationToken());
            response.Notifications.Should().ContainSingle(e => e.Message == Resource.DurationIsInvalid);
        }

        [Fact]
        public async void ShouldBeExistsTitle()
        {
            var repositoryMovie = MovieRepositoryBuilder.Instance().Exists().Build();

            AddMovieHandle handler = new AddMovieHandle(_mediator.Object, repositoryMovie);
            var response = await handler.Handle(_command, new System.Threading.CancellationToken());
            response.Notifications.Should().ContainSingle(e => e.Message == Resource.TitleExists);
        }
    }
}
