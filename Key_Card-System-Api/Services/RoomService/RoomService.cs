using Key_Card_System_Api.Repositories.RoomRepository;
using Keycard_System_API.Models;

namespace Key_Card_System_Api.Services.RoomService
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;

        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<List<Room>> GetAllRoomsAsync()
        {
            return await _roomRepository.GetAllRoomsAsync();
        }

        public async Task<Room?> GetRoomByIdAsync(int id)
        {
            return await _roomRepository.GetRoomByIdAsync(id);
        }

        public async Task UpdateRoomAccessLevelAsync(int roomId, string accessLevel)
        {
            await _roomRepository.UpdateRoomAccessLevelAsync(roomId, accessLevel);
        }
    }
}
