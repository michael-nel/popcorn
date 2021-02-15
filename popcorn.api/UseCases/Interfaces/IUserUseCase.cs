using Domain.Commands;
using Domain.Commands.User.AddUser;
using Domain.Commands.User.AuthenticateUser;
using Infra.Security;
using System.Threading.Tasks;

namespace UseCases.Interfaces
{
    public interface IUserUseCase
    {
        Task<Response> Add(AddUserRequest addUserRequest);
        Task<object> Auth(AuthenticateUserRequest request, SigningConfigurations signingConfigurations, TokenConfigurations tokenConfigurations);
    }
}
