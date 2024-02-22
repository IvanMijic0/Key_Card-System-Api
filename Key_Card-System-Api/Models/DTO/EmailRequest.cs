namespace Key_Card_System_Api.Models.DTO
{
    public class EmailRequest
    {
        public required string ToEmail { get; set; }
        public required string Subject { get; set; }
        public required string Body { get; set; }
    }
}
