using System.Collections.Generic;
using System.Threading.Tasks;
using Keycard_System_API.Models;

namespace Key_Card_System_Api.Repositories.RoomRepository
{
    public interface IRoomRepository
    {
        Task<List<Room>> GetAllRoomsAsync();
        Task<Room?> GetRoomByIdAsync(int id);
    }
}
