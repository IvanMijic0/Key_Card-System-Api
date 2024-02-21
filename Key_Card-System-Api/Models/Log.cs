using System.ComponentModel.DataAnnotations.Schema;
﻿using KeyCard_System_Api.Models;

namespace Keycard_System_API.Models
{
    public class Log
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        [Column("entry_type")]
        public string Entry_type { get; set; }

        // Foreign key properties
        public int User_id { get; set; }
        public int Room_id { get; set; }

        // Navigation properties
        [ForeignKey("User_id")]
        public virtual User User { get; set; }
        [ForeignKey("Room_id")]
        public virtual Room Room { get; set; }

        public string? Description { get; set; }

        public Log(int id, string entry_type, int user_id, int room_id, string description)
        {
            Id = id;
            Timestamp = DateTime.UtcNow;
            Entry_type = entry_type;
            User_id = user_id;
            Room_id = room_id;
            Description = description;
        }
    }
}

