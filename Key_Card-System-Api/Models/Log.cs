namespace Keycard_System_API.Models
{
    public class Log
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string Entry_type { get; set; }
        public int User_id { get; set; }
        public int Room_id { get; set; }
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
