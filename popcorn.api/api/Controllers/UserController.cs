using Domain.Commands.User.AddUser;
using Domain.Commands.User.AuthenticateUser;
using Infra.Repositories.Transactions;
using Infra.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using UseCases.Interfaces;

namespace api.Controllers
{
    public class UserController : Base.ControllerBase
    {
        private readonly IUserUseCase _userUseCase;

        public UserController(IUserUseCase userUseCase, IUnitOfWork unitOfwork) : base(unitOfwork)
        {
            _userUseCase = userUseCase;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/user")]
        public async Task<IActionResult> Add([FromBody] AddUserRequest request)
        {
            try
            {
                var response = await _userUseCase.Add(request);
                return await ResponseAsync(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/user/auth")]
        public async Task<IActionResult> Auth(
        [FromBody] AuthenticateUserRequest request,
        [FromServices] SigningConfigurations signingConfigurations,
        [FromServices] TokenConfigurations tokenConfigurations)
        {
            try
            {
                var authUserResponse = await _userUseCase.Auth(request, signingConfigurations, tokenConfigurations);

                return Ok(authUserResponse);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
