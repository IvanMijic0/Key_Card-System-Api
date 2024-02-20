using Keycard_System_API.Data;
using Keycard_System_API.Data;
using Keycard_System_API.Models;
using Keycard_System_API.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Keycard_System_API.Repositories
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
