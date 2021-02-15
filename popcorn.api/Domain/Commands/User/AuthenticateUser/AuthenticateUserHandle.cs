using System.Threading;
using System.Threading.Tasks;
using Domain.Extensions;
using Domain.Interfaces.Repositories;
using Domain.Resources;
using MediatR;
using prmToolkit.NotificationPattern;

namespace Domain.Commands.User.AuthenticateUser
{
    public class AuthenticateUserHandle : Notifiable, IRequestHandler<AuthenticateUserRequest, AuthenticateUserResponse>
    {
        private readonly IMediator _mediator;
        private readonly IRepositoryUser _repositoryUser;
        public AuthenticateUserHandle(IMediator mediator, IRepositoryUser repositoryUser)
        {
            _mediator = mediator;
            _repositoryUser = repositoryUser;
        }
        public async Task<AuthenticateUserResponse> Handle(AuthenticateUserRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                AddNotification("Request", Resource.RequestNotbeNull);
                return null;
            }

            request.Password = request.Password.ConvertToMD5();

            Entities.User user = _repositoryUser.GetBy(x => x.Email == request.Email && x.Password == request.Password);

            if (user == null)
            {
                AddNotification("Usuario", Resource.UserNotFound);

                return new AuthenticateUserResponse()
                {
                    Authenticated = false
                };
            }

            var response = (AuthenticateUserResponse)user;

            return await Task.FromResult(response);
        }
    }
}
