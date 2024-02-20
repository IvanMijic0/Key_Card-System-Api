using Keycard_System_API.Models;

namespace Key_Card_System_Api.Services.RoomService
{
    public interface IRoomService
    {
        List<Room> GetAllRooms();
        Room? GetRoomById(int id);
    }
}
