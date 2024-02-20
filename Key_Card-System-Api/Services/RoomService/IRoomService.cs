using Keycard_System_API.Models;

namespace Key_Card_System_Api.Services.RoomService
{
    public interface IRoomService
    {
        Task<List<Room>> GetAllRoomsAsync();
        Task<Room?> GetRoomByIdAsync(int id);
    }
}
