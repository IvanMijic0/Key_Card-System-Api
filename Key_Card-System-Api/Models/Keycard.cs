using System.ComponentModel.DataAnnotations.Schema;

namespace Key_Card_System_Api.Models
{
    [Table("key_cards")]
    public class Keycard
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("key_id")]
        public string Key_Id { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; } = true;

        public Keycard()
        {
        }

        public Keycard(string key_id)
        {
            Key_Id = key_id;
        }
    }
}
