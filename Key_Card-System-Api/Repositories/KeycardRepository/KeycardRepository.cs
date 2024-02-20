using Key_Card_System_Api.Models;
using Keycard_System_API.Data;
using Microsoft.EntityFrameworkCore;


namespace Key_Card_System_Api.Repositories.KeycardRepository
{
    public class KeycardRepository : IKeycardRepository
    {
        private readonly ApplicationDbContext _context;

        public KeycardRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Keycard>> GetAllKeycardsAsync()
        {
            return await _context.Keycards.ToListAsync();
        }

        public async Task<Keycard?> GetKeycardByIdAsync(int id)
        {
            return await _context.Keycards.FirstOrDefaultAsync(k => k.Id == id);
        }

        public async Task CreateKeycardAsync(Keycard keycard)
        {
            _context.Keycards.Add(keycard);
            await _context.SaveChangesAsync();
        }

        public async Task<Keycard> UpdateKeycardAsync(Keycard keycard)
        {
            _context.Keycards.Update(keycard);
            await _context.SaveChangesAsync();
            return keycard;
        }

        public async Task<bool> DeleteKeycardAsync(int id)
        {
            var keycard = await _context.Keycards.FindAsync(id);
            if (keycard != null)
            {
                _context.Keycards.Remove(keycard);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
