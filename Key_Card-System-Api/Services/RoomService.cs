using Keycard_System_API.Models;
using Keycard_System_API.Repositories;

namespace Keycard_System_API.Services
{
    public class RoomService:IRoomService
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
