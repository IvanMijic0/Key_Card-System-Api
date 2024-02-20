namespace Keycard_System_API.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Access_level { get; set; }
        public int Device_id { get; set; }

        public Room(int id, string name, string access_level, int device_id)
        {
            Id = id;
            Name = name;
            Access_level = access_level;
            Device_id = device_id;
        }
    }
}
