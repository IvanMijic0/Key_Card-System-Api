﻿using Key_Card_System_Api.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Keycard_System_API.Models
{
    public class User
    {
        public int Id { get; set; }
        [Column("first_name")]
        public string FirstName { get; set; }
        [Column("last_name")]
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public int Key_Id { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
        [Column("in_room")]
        public bool InRoom { get; set; } = false;
        [Column("current_room_id")]
        public int CurrentRoomId { get; set; }
        [Column("first_login")]
        public bool FirstLogin { get; set; } = false;

        [ForeignKey("Key_Id")]
        public Keycard Keycard { get; set; }

        public User()
        {
        }

        public User(string username, string first_name, string last_name, string email, int key_id, string passwordHash, string role = "Employee")
        {
            FirstName = first_name;
            LastName = last_name;
            Username = username;
            Email = email;
            Key_Id = key_id;
            PasswordHash = passwordHash;
            Role = role;
        }
    }
}
