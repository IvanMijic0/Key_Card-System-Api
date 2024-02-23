namespace Key_Card_System_Api.Models.DTO
{
    public class ChangePasswordModel
    {
        public int UserId { get; set; }
        public required string CurrentPassword { get; set; }
        public required string NewPassword { get; set; }
    }
}
