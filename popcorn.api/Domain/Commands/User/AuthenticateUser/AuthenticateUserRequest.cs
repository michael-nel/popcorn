using MediatR;

namespace Domain.Commands.User.AuthenticateUser
{
    public class AuthenticateUserRequest : IRequest<AuthenticateUserResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
