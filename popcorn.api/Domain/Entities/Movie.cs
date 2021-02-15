using Domain.Entities.Base;
using Domain.Resources;
using prmToolkit.NotificationPattern;
using System;

namespace Domain.Entities
{
    public class Movie : EntityBase
    {
        protected Movie() { }
        public Movie(string image, string title, string description, TimeSpan duration)
        {
            Image = image;
            Title = title;
            Description = description;
            Duration = duration;

            new AddNotifications<Movie>(this)
                .IfNullOrEmpty(x => x.Image, Resource.ImageIsRequired)
                .IfNullOrInvalidLength(x => x.Title, 3, 250, Resource.TitleMustBeBetween3And250)
                .IfNullOrInvalidLength(x => x.Description, 3, 500, Resource.DescriptionMusteBeBetween3And500);
        }
        public void Update(string image, string title, string description, TimeSpan duration)
        {
            Image = image;
            Title = title;
            Description = description;
            Duration = duration;

            new AddNotifications<Movie>(this)
                .IfNullOrEmpty(x => x.Image, "Imagem Requerida.")
                .IfNullOrInvalidLength(x => x.Title, 3, 250, "Titulo deve conter entre 3 e 250 caracteres")
                .IfNullOrInvalidLength(x => x.Description, 3, 500, "Primeiro nome deve conter entre 3 e 500 caracteres");
        }
        public string Image { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }
      
    }
}
