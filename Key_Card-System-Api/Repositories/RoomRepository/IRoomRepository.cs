using Keycard_System_API.Models;

namespace Key_Card_System_Api.Repositories.RoomRepository
{
    public interface IRoomRepository
    {
        List<Room> GetAllRooms();
        Room? GetRoomById(int id);
    }
}
