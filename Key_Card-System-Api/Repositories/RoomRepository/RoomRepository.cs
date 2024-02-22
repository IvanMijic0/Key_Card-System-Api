using Keycard_System_API.Data;
using Keycard_System_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Key_Card_System_Api.Repositories.RoomRepository
{
    public class RoomRepository : IRoomRepository
    {
        private readonly ApplicationDbContext _context;

        public RoomRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Room>> GetAllRoomsAsync()
        {
            return await _context.room.ToListAsync();
        }

        public async Task<Room?> GetRoomByIdAsync(int id)
        {
            return await _context.room.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task UpdateRoomAccessLevelAsync(int roomId, string accessLevel)
        {
            var room = await _context.room.FindAsync(roomId) ?? throw new ArgumentException("Room not found.");
            room.Access_level = accessLevel;
            await _context.SaveChangesAsync();
        }
    }
}
