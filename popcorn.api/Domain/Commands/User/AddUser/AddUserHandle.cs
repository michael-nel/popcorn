using System.Threading;
using System.Threading.Tasks;
using Domain.Interfaces.Repositories;
using Domain.Resources;
using MediatR;
using prmToolkit.NotificationPattern;

namespace Domain.Commands.User.AddUser
{
    public class AddUserHandle : Notifiable, IRequestHandler<AddUserRequest, Response>
    {
        private readonly IMediator _mediator;
        private readonly IRepositoryUser _repositoryUser;
        public AddUserHandle(IMediator mediator, IRepositoryUser repositoryUsuario)
        {
            _mediator = mediator;
            _repositoryUser = repositoryUsuario;
        }

        public async Task<Response> Handle(AddUserRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                AddNotification("Request", Resource.RequestNotbeNull);
                return new Response(this);
            }

            //Verificar se o usuario existe
            if (_repositoryUser.Exists(x => x.Email == request.Email))
            {
                AddNotification("Email", Resource.EmailExists);
                return new Response(this);
            }

            Entities.User user = new Entities.User(request.FirstName, request.LastName, request.Email, request.Password);
            AddNotifications(user);

            if (IsInvalid())
            {
                return new Response(this);
            }
            user = _repositoryUser.Add(user);

            AddUserNotification addUserNotification = new AddUserNotification(user);
            var response = new Response(this, user);

            await _mediator.Publish(addUserNotification);
            return await Task.FromResult(response);
        }
    }
}
