using Bogus;
using Domain.Entities;
using Domain.Resources;
using ExpectedObjects;
using FluentAssertions;
using System;
using Test.Builders;
using Xunit;

namespace Test.Entities
{
    public class MovieTest
    {
        private readonly Faker _faker;

        public MovieTest()
        {
            _faker = new Faker();
        }

        [Fact]
        public void ShouldBeCreateMovie()
        {
            var movieShould = new
            {
                Image = _faker.Image.ToString(),
                Title = _faker.Lorem.Text(),
                Description = _faker.Lorem.Paragraph(5),
                Duration = _faker.Date.Timespan(null)
            };

            var movie = new Movie(movieShould.Image, movieShould.Title, movieShould.Description, movieShould.Duration);

            movieShould.ToExpectedObject().ShouldMatch(movie);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ShouldBeNotCreateWithImage(string image)
        {
            var movie = MovieBuilder.New().WithImage(image).Build();
            movie.Notifications.Should().ContainSingle(e => e.Message == Resource.ImageIsRequired);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("1")]
        public void ShouldbeNotCreateWithTitle(string title)
        {
            var movie = MovieBuilder.New().WithTitle(title).Build();
            movie.Notifications.Should().ContainSingle(e => e.Message == Resource.TitleMustBeBetween3And250);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("1")]
        public void ShouldbeNotCreateWithDescription(string description)
        {
            var movie = MovieBuilder.New().WithDescription(description).Build();
            movie.Notifications.Should().ContainSingle(e => e.Message == Resource.DescriptionMusteBeBetween3And500);
        }
    }
}
