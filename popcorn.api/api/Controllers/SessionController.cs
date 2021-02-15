using Domain.Commands.Movie.AddMovie;
using Domain.Commands.Session.AddSession;
using Domain.Commands.Session.DeleteSession;
using Domain.Commands.Session.GetSessions;
using Infra.Repositories.Transactions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using UseCases.Interfaces;

namespace api.Controllers
{
    public class SessionController : Base.ControllerBase
    {
        private readonly ISessionUseCase _sessionUseCase;

        public SessionController(ISessionUseCase sessionUseCase, IUnitOfWork unitOfwork) : base(unitOfwork)
        {
            _sessionUseCase = sessionUseCase;
        }

        [Authorize]
        [HttpPost]
        [Route("api/session")]
        public async Task<IActionResult> AddSession([FromBody] AddSessionRequest request)
        {
            try
            {
                var response = await _sessionUseCase.Add(request);
                return await ResponseAsync(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Authorize]
        [HttpDelete]
        [Route("api/session/{id:Guid}")]
        public async Task<IActionResult> DeleteSession(Guid id)
        {
            try
            {
                var request = new DeleteSessionRequest(id);
                var response = await _sessionUseCase.Delete(request);
                return await ResponseAsync(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("api/session")]
        [Route("api/session/{pages:int}")]
        public async Task<IActionResult> GetSessionAll(int pages)
        {
            try
            {
                var request = new GetSessionsRequest(pages);
                var response = await _sessionUseCase.GetList(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
