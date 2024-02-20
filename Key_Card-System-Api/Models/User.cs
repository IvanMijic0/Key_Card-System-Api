using System;

namespace Keycard_System_API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public int? Key_Id { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;

        public User()
        {
        }

        public User(string username, string email, int key_id, string passwordHash, string role = "Employee")
        {
            Username = username;
            Email = email;
            Key_Id = key_id;
            PasswordHash = passwordHash;
            Role = role;
        }
    }
}
