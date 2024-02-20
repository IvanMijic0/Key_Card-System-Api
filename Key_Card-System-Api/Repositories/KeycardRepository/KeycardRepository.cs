using Key_Card_System_Api.Models;
using Keycard_System_API.Data;

namespace Key_Card_System_Api.Repositories.KeycardRepository
{
    public class KeycardRepository(ApplicationDbContext context) : IKeycardRepository
    {
        private readonly ApplicationDbContext _context = context;

        public List<Keycard> GetAllKeycards()
        {
            return [.. _context.Keycards];
        }

        public Keycard? GetKeycardById(int id)
        {
            return _context.Keycards.FirstOrDefault(k => k.Id == id);
        }

        public void CreateKeycard(Keycard keycard)
        {
            _context.Keycards.Add(keycard);
            _context.SaveChanges();
        }

        public Keycard UpdateKeycard(Keycard keycard)
        {
            _context.Keycards.Update(keycard);
            _context.SaveChanges();
            return keycard;
        }

        public bool DeleteKeycard(int id)
        {
            var keycard = _context.Keycards.Find(id);
            if (keycard != null)
            {
                _context.Keycards.Remove(keycard);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
