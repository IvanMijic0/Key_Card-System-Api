using Key_Card_System_Api.Repositories.KeycardRepository;
using Key_Card_System_Api.Repositories.LogRepositroy;
using Key_Card_System_Api.Repositories.RoomRepository;
using Key_Card_System_Api.Repositories.UserRepository;
using Keycard_System_API.Models;

namespace Key_Card_System_Api.Services.LogService
{
    public class LogService : ILogService
    {
        private readonly ILogRepository _logRepository;
        private readonly IKeycardRepository _keycardRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IUserRepository _userRepository;

        public LogService(ILogRepository logRepository, IKeycardRepository keycardRepository, IRoomRepository roomRepository, IUserRepository userRepository
            )
        {
            _logRepository = logRepository;
            _keycardRepository = keycardRepository;
            _roomRepository = roomRepository;
            _userRepository = userRepository;
        }

        public List<Log> GetAllLogs()
        {
            return _logRepository.GetAllLogs();
        }

        public Log AddLog(Log log)
        {
            ArgumentNullException.ThrowIfNull(log);

            var user = _userRepository.GetUserById(log.User_id) ?? throw new ArgumentException("Invalid user ID.");

            var keycard = _keycardRepository.GetKeycardById(user.Key_Id) ?? throw new ArgumentException("Invalid key card.");

            if (!ValidateAccess(keycard.AccessLevel, log.Room_id))
            {
                throw new InvalidOperationException("Access level does not match.");
            }

            log.User_id = keycard.Id;
            log.Room_id = log.Room_id;

            return _logRepository.AddLog(log);
        }

        private bool ValidateAccess(string accessLevel, int roomId)
        {
            var room = _roomRepository.GetRoomById(roomId) ?? throw new ArgumentException("Invalid room ID.");
            if (!string.Equals(accessLevel, room.Access_level, StringComparison.OrdinalIgnoreCase))
            {
                return false; // Access level does not match
            }

            return true; // Access allowed
        }
    }
}
