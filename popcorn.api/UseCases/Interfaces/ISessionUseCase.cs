using Domain.Commands;
using Domain.Commands.Session.AddSession;
using Domain.Commands.Session.DeleteSession;
using Domain.Commands.Session.GetSessions;
using System.Threading.Tasks;

namespace UseCases.Interfaces
{
    public interface ISessionUseCase
    {
        Task<Response> Add(AddSessionRequest addUserRequest);
        Task<GetSessionsResponse> GetList(GetSessionsRequest getSessionsRequest);
        Task<Response> Delete(DeleteSessionRequest deleteSessionRequest);
    }
}
