using Domain.Commands;
using Domain.Commands.User.AddUser;
using Domain.Commands.User.AuthenticateUser;
using Infra.Security;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Interfaces;

namespace UseCases
{
    public class UserUseCase : IUserUseCase
    {
        private readonly IMediator _mediator;
        public UserUseCase(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<Response> Add(AddUserRequest addUserRequest)
        {
            var response = await _mediator.Send(addUserRequest, CancellationToken.None);
            return response;
        }
        public async Task<object> Auth(AuthenticateUserRequest request, SigningConfigurations signingConfigurations, TokenConfigurations tokenConfigurations)
        {
            var authUserResponse = await _mediator.Send(request, CancellationToken.None);

            if (authUserResponse.Authenticated == true)
            {
                var response = GenerateToken(authUserResponse, signingConfigurations, tokenConfigurations);

                return response;
            }
            else
            {
                throw new UnauthorizedAccessException();
            }
        }
        private object GenerateToken(AuthenticateUserResponse response, SigningConfigurations signingConfigurations, TokenConfigurations tokenConfigurations)
        {
            if (response.Authenticated == true)
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(response.Id.ToString(), "Id"),
                    new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim("Usuario", JsonConvert.SerializeObject(response))
                    }
                );

                DateTime dateCreated = DateTime.Now;
                DateTime dateExpiration = dateCreated + TimeSpan.FromSeconds(tokenConfigurations.Seconds);

                var handler = new JwtSecurityTokenHandler();
                var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                {
                    Issuer = tokenConfigurations.Issuer,
                    Audience = tokenConfigurations.Audience,
                    SigningCredentials = signingConfigurations.SigningCredentials,
                    Subject = identity,
                    NotBefore = dateCreated,
                    Expires = dateExpiration
                });
                var token = handler.WriteToken(securityToken);

                return new
                {
                    authenticated = true,
                    created = dateCreated.ToString("yyyy-MM-dd HH:mm:ss"),
                    expiration = dateExpiration.ToString("yyyy-MM-dd HH:mm:ss"),
                    accessToken = token,
                    message = "OK",
                    firstName = response.Name
                };
            }
            else
            {
                return response;
            }
        }
    }
}
