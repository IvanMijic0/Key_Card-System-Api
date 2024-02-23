using Key_Card_System_Api.Models;
using Keycard_System_API.Data;
using Keycard_System_API.Models;
using Microsoft.EntityFrameworkCore;
using System;


namespace Key_Card_System_Api.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users
                .Include(user => user.Keycard)
                .ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users.Include(u => u.Keycard).FirstOrDefaultAsync(u => u.Id == id);
        }



        public async Task CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task UpdateUsersKeyCardAsync(int user_id, string response, string access_level)
        {
            var user = await _context.Users.FindAsync(user_id);

            if (user != null)
            {
                var keyCard = await _context.Keycards.FirstOrDefaultAsync(k => k.Id == user.Key_Id);

                if (keyCard != null)
                {
                    var notification = await _context.notifications.FirstOrDefaultAsync(n => n.User_id == user_id && n.Is_active == 1);
                    if (notification != null)
                    { 
                        notification.Is_active = 0;
                        _context.notifications.Update(notification);
                    }
                    if(response == "approve")
                    if (response == "approve")
                    {
                        if(access_level == "Admin" || access_level == "Manager" || access_level == "Low" || access_level == "Medium" || access_level == "High")
                        {
                            keyCard.AccessLevel = access_level;
                        }
                        else
                        {
                            Random random = new Random();

                            int dodatniBrojevi = random.Next(0, 999999);
                            string osmobitniBroj = keyCard.Id.ToString() + "000" + dodatniBrojevi.ToString("D6");
                            keyCard.Key_Id = osmobitniBroj;
                        }

                        _context.Keycards.Update(keyCard);
                    }

                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Nije pronađen odgovarajući key_card za korisnika.");
                }
            }
            else
            {
                throw new Exception("Korisnik nije pronađen.");
            }
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
                .Include(u => u.Keycard)
                .FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<List<User>> SearchUsersByUsernameAsync(string searchTerm)
        {
            var lowercaseSearchTerm = searchTerm.ToLower();
            return await _context.Users
                .Include(u => u.Keycard)
                .Where(u => u.Username.Contains(lowercaseSearchTerm, StringComparison.CurrentCultureIgnoreCase))
                .ToListAsync();
        }

        public async Task<List<User>> SearchUsersByKeyIdAsync(string searchTerm)
        {
            if (!int.TryParse(searchTerm, out int keyId))
            {
                return [];
            }

            return await _context.Users
                .Include(u => u.Keycard)
                .Where(u => u.Key_Id == keyId)
                .ToListAsync();
        }

        public async Task UpdateUsersKeyCardAcessLevelAsync(int userId, string accessLevel)
        {
            var user = await _context.Users
                .Include(u => u.Keycard)
                .FirstOrDefaultAsync(u => u.Id == userId) ?? throw new Exception("User not found.");
            var keycard = user.Keycard ?? throw new Exception("Keycard not found for this user.");
            keycard.AccessLevel = accessLevel;
            keycard.PreviousAccessLevel = accessLevel;

            await _context.SaveChangesAsync();
        }

    }
}
