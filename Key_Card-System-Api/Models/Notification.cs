using Keycard_System_API.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Key_Card_System_Api.Models
{
    public class Notification
    {
        public int Id { get; set; }

        public int User_id { get; set; }

        [ForeignKey("User_id")]
        public virtual User User { get; set; }

        public string Message { get; set; }
        public string Type { get; set; }

        public string Access_level { get; set; }

        public int Is_active { get; set; }

        public Notification(int id, int user_id, string message, string type, string access_level, int is_active)
        {
            Id = id;
            User_id = user_id;
            Message = message;
            Type = type;
            Access_level = access_level;
            Is_active = is_active;
        }
    }
}
