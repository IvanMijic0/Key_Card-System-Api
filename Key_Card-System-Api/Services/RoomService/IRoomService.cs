using Keycard_System_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Key_Card_System_Api.Services.RoomService
{
    public interface IRoomService
    {
        Task<List<Room>> GetAllRoomsAsync();
        Task<Room?> GetRoomByIdAsync(int id);
    }
}
