namespace Key_Card_System_Api.Models.DTO
{
    public class NotificationAdd
    {
        public int User_Id { get; set; }
        public required string Type_of_request { get; set; }

        public required string Access_level{ get; set; }
    }
}
