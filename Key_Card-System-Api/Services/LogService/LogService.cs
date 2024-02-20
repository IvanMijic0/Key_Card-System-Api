using Keycard_System_API.Models;
using Keycard_System_API.Models.DTO;
using Key_Card_System_Api.Repositories.KeycardRepository;
using Key_Card_System_Api.Repositories.LogRepositroy;
using Key_Card_System_Api.Repositories.RoomRepository;
using Key_Card_System_Api.Repositories.UserRepository;

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
            return await EnhanceLogEntriesAsync(logs);
        }
        public async Task<Log> AddLogAsync(Log log)
        {
            ArgumentNullException.ThrowIfNull(log);

            var user = await _userRepository.GetUserByIdAsync(log.User_id) ?? throw new ArgumentException("Invalid user ID.");

            var keycard = await _keycardRepository.GetKeycardByIdAsync(user.Key_Id) ?? throw new ArgumentException("Invalid key card.");

            if (!await ValidateAccess(keycard.AccessLevel, log.Room_id))
            {
                throw new InvalidOperationException("Access level does not match.");
            }

            log.User_id = keycard.Id;
            log.Room_id = log.Room_id;

            return await _logRepository.AddLogAsync(log);
        }

        private async Task<List<LogDto>> EnhanceLogEntriesAsync(List<Log> logs)
        {
            var logDtos = new List<LogDto>();

            var userIds = logs.Select(l => l.User_id).Distinct().ToList();
            var roomIds = logs.Select(l => l.Room_id).Distinct().ToList();

            var users = await _userRepository.GetAllUsersAsync();
            var rooms = await _roomRepository.GetAllRoomsAsync();

            foreach (var log in logs)
            {
                var user = users.FirstOrDefault(u => u.Id == log.User_id);
                var room = rooms.FirstOrDefault(r => r.Id == log.Room_id);

                var logDto = new LogDto
                {
                    Id = log.Id,
                    Timestamp = log.Timestamp,
                    EntryType = log.Entry_type,
                    Description = log.Description,
                    UserFirstName = user != null ? user.FirstName : "Unknown",
                    UserLastName = user != null ? user.LastName : "Unknown",
                    RoomName = room != null ? room.Name : "Unknown"
                };

                logDtos.Add(logDto);
            }

            return logDtos;
        }

        private async Task<bool> ValidateAccess(string accessLevel, int roomId)
        {
            var room = await _roomRepository.GetRoomByIdAsync(roomId);
            if (room == null)
            {
                throw new ArgumentException("Invalid room ID.");
            }

            return string.Equals(accessLevel, room.Access_level, StringComparison.OrdinalIgnoreCase);
        }
    }
}
