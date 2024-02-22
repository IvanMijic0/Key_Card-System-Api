namespace Key_Card_System_Api.Models.DTO
{
    public class NotificationRequest
    {

        public int Id { get; set; }
        public required string UserFirstName { get; set; }

        public required string UserSecondName { get; set; }
        public required string Access_level { get; set; }
    }
}
