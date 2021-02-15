using Domain.Interfaces.Repositories;
using Domain.Resources;
using MediatR;
using prmToolkit.NotificationPattern;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Commands.Movie.AddMovie
{
    public class AddMovieHandle : Notifiable, IRequestHandler<AddMovieRequest, Response>
    {
        private readonly IMediator _mediator;
        private readonly IRepositoryMovie _repositoryMovie;
       
        public AddMovieHandle(IMediator mediator, IRepositoryMovie repositoryMovie)
        {
            _mediator = mediator;
            _repositoryMovie = repositoryMovie;
        }

        public async Task<Response> Handle(AddMovieRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                AddNotification("Request", Resource.RequestNotbeNull);
                return new Response(this);
            }

            if (_repositoryMovie.Exists(x => x.Title == request.Title))
            {
                AddNotification("Titulo", Resource.TitleExists);
                return new Response(this);
            }

            if(string.IsNullOrEmpty(request.Duration) || TimeSpan.Zero == TimeSpan.Parse(request.Duration))
            {
                AddNotification("Duração", Resource.DurationIsInvalid);
                return new Response(this);
            }
           
            //request.Duration
            Entities.Movie user = new Entities.Movie(request.Image, request.Title, request.Description, TimeSpan.Parse(request.Duration));
            AddNotifications(user);

            if (IsInvalid())
            {
                return new Response(this);
            }

            user = _repositoryMovie.Add(user);

            AddMovieNotification addMovieNotification = new AddMovieNotification(user);
            var response = new Response(this, user);

            await _mediator.Publish(addMovieNotification);
            return await Task.FromResult(response);
        }
    }
}
