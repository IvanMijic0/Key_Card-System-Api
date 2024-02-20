namespace Keycard_System_API.Models.DTO
{
    public class LogDto
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public required string EntryType { get; set; }
        public required string UserFirstName { get; set; }
        public required string UserLastName { get; set; }
        public required string RoomName { get; set; }
        public required string Description { get; set; }
    }
}
