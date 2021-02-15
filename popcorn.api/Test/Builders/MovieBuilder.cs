using Bogus;
using Domain.Entities;
using System;

namespace Test.Builders
{
    public class MovieBuilder
    {

        protected Guid Id;
        protected string Image;
        protected string Title;
        protected string Description;
        protected TimeSpan Duration;

        public Movie Build()
        {
            var movie = new Movie(Image, Title, Description, Duration);

            if (Id == null ) return movie;
            var propertyInfo = movie.GetType().GetProperty("Id");
            propertyInfo.SetValue(movie, Convert.ChangeType(Id, propertyInfo.PropertyType), null);
            return movie;
        }


        public static MovieBuilder New()
        {
            var faker = new Faker();

            return new MovieBuilder
            {
                Image = faker.Image.ToString(),
                Title = faker.Lorem.Text(),
                Description = faker.Lorem.Paragraph(5),
                Duration = TimeSpan.FromHours(1)
            };
        }

        public MovieBuilder WithId(Guid id)
        {
            Id = id;
            return this;
        }
        public MovieBuilder WithImage(string image)
        {
            Image = image;
            return this;
        }

        public MovieBuilder WithTitle(string title)
        {
            Title = title;
            return this;
        }

        public MovieBuilder WithDescription(string description)
        {
            Description = description;
            return this;
        }

        public MovieBuilder WithDuration(TimeSpan duration)
        {
            Duration = duration;
            return this;
        }

    }
}
