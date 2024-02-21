using Keycard_System_API.Models;
using Keycard_System_API.Models.DTO;
using Key_Card_System_Api.Repositories.KeycardRepository;
using Key_Card_System_Api.Repositories.LogRepositroy;
using Key_Card_System_Api.Repositories.RoomRepository;
using Key_Card_System_Api.Repositories.UserRepository;
using Key_Card_System_Api.Models.DTO;

namespace Key_Card_System_Api.Services.LogService
{
    public class LogService : ILogService
    {
        private readonly ILogRepository _logRepository;
        private readonly IKeycardRepository _keycardRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IUserRepository _userRepository;

        public LogService(ILogRepository logRepository, IKeycardRepository keycardRepository, IRoomRepository roomRepository, IUserRepository userRepository)
        {
            _logRepository = logRepository;
            _keycardRepository = keycardRepository;
            _roomRepository = roomRepository;
            _userRepository = userRepository;
        }

        public async Task<List<LogDto>> GetAllLogsAsync()
        {
            var logs = await _logRepository.GetAllLogsAsync();
            var logDtos = new List<LogDto>();

            foreach (var log in logs)
            {
                var user = log.User;
                var room = log.Room;

                var logDto = new LogDto
                {
                    Id = log.Id,
                    Timestamp = log.Timestamp,
                    EntryType = log.Entry_type,
                    Description = log.Description!,
                    UserFirstName = user != null ? user.FirstName : "Unknown",
                    UserLastName = user != null ? user.LastName : "Unknown",
                    RoomName = room != null ? room.Name : "Unknown"
                };

                logDtos.Add(logDto);
            }

            return logDtos;
        }

        public async Task<Log> AddLogAsync(LogRequestModel logRequest)
        {
            ArgumentNullException.ThrowIfNull(logRequest);

            var user = await _userRepository.GetUserByIdAsync(logRequest.User_Id);
            if (user == null)
            {
                return new Log(0, "Error", logRequest.User_Id, logRequest.Room_Id, "Invalid user ID.");
            }

            var room = await _roomRepository.GetRoomByIdAsync(logRequest.Room_Id);
            if (room == null)
            {
                return new Log(0, "Error", logRequest.User_Id, logRequest.Room_Id, "Invalid room ID.");
            }

            string accessStatus = "denied";
            if (logRequest.Entry_Type == "In")
            {
                accessStatus = "granted";
            }

            string description = $"Attempted access to room {logRequest.Room_Id}. Access {accessStatus} for user {user.FirstName} {user.LastName} with key card ID {user.Key_Id}";

            var log = new Log(0, logRequest.Entry_Type, logRequest.User_Id, logRequest.Room_Id, description);
            log.User = user;
            log.Room = room;

            var addedLog = await _logRepository.AddLogAsync(log);

            return addedLog;
        }

        public async Task<List<Log>> SearchLogsAsync(string searchTerm)
        {
             return await _logRepository.SearchLogsAsync(searchTerm);   
        }

        private async Task<bool> ValidateAccess(string accessLevel, int roomId)
        {
            var room = await _roomRepository.GetRoomByIdAsync(roomId) ?? throw new ArgumentException("Invalid room ID.");
            return string.Equals(accessLevel, room.Access_level, StringComparison.OrdinalIgnoreCase);
        }
    }
}
