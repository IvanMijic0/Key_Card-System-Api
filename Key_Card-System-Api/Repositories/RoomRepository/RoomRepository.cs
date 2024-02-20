using Keycard_System_API.Data;
using Keycard_System_API.Models;

namespace Key_Card_System_Api.Repositories.RoomRepository
{
    public class RoomRepository(ApplicationDbContext context) : IRoomRepository
    {
        private readonly ApplicationDbContext _context = context;

        public List<Room> GetAllRooms()
        {
            return [.. _context.room];
        }
        public Room? GetRoomById(int id)
        {
            var room = _context.room.FirstOrDefault(u => u.Id == id);
            if (room == null)
            {
                return null;
            }
            return room;
        }
    }
}
