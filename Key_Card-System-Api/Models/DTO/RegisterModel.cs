namespace Key_Card_System_Api.Models.DTO
{
    public class RegisterModel
    {
        public required string Email { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string Key_Id { get; set; }
        public string? Role { get; set; }
        public required string Access_Level { get; set; }
    }
}
