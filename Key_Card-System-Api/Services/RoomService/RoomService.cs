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

        public List<Room> GetAllRooms()
        {
            return _roomRepository.GetAllRooms();
        }

        public Room? GetRoomById(int id)
        {
            return _roomRepository.GetRoomById(id);
        }
    }
}
