namespace Key_Card_System_Api.Models.DTO
{
    public class LogRequestModel
    {
        public required string Entry_Type { get; set; }
        public int User_Id { get; set; }
        public int Room_Id { get; set; }
    }
}
