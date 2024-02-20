using Keycard_System_API.Models;

namespace Key_Card_System_Api.Repositories.LogRepositroy
{
    public interface ILogRepository
    {
        List<Log> GetAllLogs();

        int CountLogs();

        int CountLogs(int room_id);

        int CountErrors();


    }
}
