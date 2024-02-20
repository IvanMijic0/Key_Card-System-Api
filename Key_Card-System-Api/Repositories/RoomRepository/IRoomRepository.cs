using Keycard_System_API.Models;

namespace Keycard_System_API.Repositories
{
    public interface IRoomRepository
    {
        List<Room> GetAllRooms();
        Room? GetRoomById(int id);
    }
}
