using Domain.Commands;
using Domain.Commands.Room.GetRoom;
using System.Threading.Tasks;

namespace UseCases.Interfaces
{
    public interface IRoomUseCase
    {
        Task<Response> Get(GetRoomRequest getRoomRequest);
    }
}
