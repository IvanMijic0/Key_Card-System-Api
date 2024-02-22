using Key_Card_System_Api.Models.DTO;
using Key_Card_System_Api.Repositories.KeycardRepository;
using Key_Card_System_Api.Repositories.LogRepositroy;
using Key_Card_System_Api.Repositories.RoomRepository;
using Key_Card_System_Api.Repositories.UserRepository;
using Keycard_System_API.Models;
using Keycard_System_API.Models.DTO;

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

        public async Task<List<LogCounts>> GetCountOflogsWithRoomsAsync()
        {
            List<Log> logs = await _logRepository.GetAllLogsAsync();
            var logCounts = new List<LogCounts>();

            var countByRoomId = logs
                .GroupBy(log => log.Room_id)
                .Select(group => new { RoomId = group.Key, Count = group.Count() })
                .ToDictionary(item => item.RoomId, item => item.Count);

            foreach (var kvp in countByRoomId)
            {
                int roomId = kvp.Key;
                int numberOfLogs = kvp.Value;

                var logWithRoom = logs.FirstOrDefault(log => log.Room_id == roomId);

                string roomName = logWithRoom!.Room.Name;

                LogCounts logCount = new()
                {
                    Id = roomId,
                    RoomName = roomName,
                    NumberOfLogs = numberOfLogs
                };

                logCounts.Add(logCount);
            }
            return logCounts;
        }

        public async Task<List<LogDto>> GetLogsByRoomIdAsync(int room_id)
        {
            var logs = await _logRepository.GetLogsByRoomIdAsync(room_id);
            var logDtos = new List<LogDto>();

            foreach (var log in logs.OrderByDescending(l => l.Id))
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

        public async Task<List<LogDto>> GetLogsByUserIdAsync(int user_id)
        {
            var logs = await _logRepository.GetLogsByUserIdAsync(user_id);
            var logDtos = new List<LogDto>();

            foreach (var log in logs.OrderByDescending(l => l.Id))
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

        public async Task<List<LogDto>> GetAllLogsAsync()
        {
            var logs = await _logRepository.GetAllLogsAsync();
            var logDtos = new List<LogDto>();

            foreach (var log in logs.OrderByDescending(l => l.Id))
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

        public async Task<List<LogDto>> GetLogsInRoom()
        {
            var logs = await _logRepository.GetLatestLogsWhereUserInRoomAsync();
            var logDtos = new List<LogDto>();

            foreach (var log in logs.OrderByDescending(l => l?.Id))
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
                var errorLog = new Log(0, "Error", logRequest.User_Id, logRequest.Room_Id, "Invalid user ID.");
                await _logRepository.AddLogAsync(errorLog);
                return errorLog;
            }

            var room = await _roomRepository.GetRoomByIdAsync(logRequest.Room_Id);
            if (room == null)
            {
                var errorLog = new Log(0, "Error", logRequest.User_Id, logRequest.Room_Id, "Invalid room ID.");
                await _logRepository.AddLogAsync(errorLog);
                return errorLog;
            }

            var keycard = await _keycardRepository.GetKeycardByIdAsync(user.Key_Id);
            if (keycard == null)
            {
                var errorLog = new Log(0, "Error", logRequest.User_Id, logRequest.Room_Id, "User's keycard not found.");
                await _logRepository.AddLogAsync(errorLog);
                return errorLog;
            }

            bool accessGranted = await ValidateAccess(keycard.AccessLevel, room.Access_level);
            string accessStatus = accessGranted ? "granted" : "denied";

            string description = $"Attempted access to room {room.Name}. Access {accessStatus} for user {user.FirstName} {user.LastName} with key card ID {keycard.Id}";

            if (logRequest.Entry_Type == "In")
            {
                user.InRoom = true;
            }
            else if (logRequest.Entry_Type == "Out")
            {
                user.InRoom = false;
            }

            var log = new Log(0, logRequest.Entry_Type, logRequest.User_Id, logRequest.Room_Id, description)
            {
                User = user,
                Room = room
            };

            if (!accessGranted)
            {
                description = $"Attempted access to room {room.Name}. Access level does not match for user {user.FirstName} {user.LastName} with key card ID {keycard.Id}";
                var errorLog = new Log(0, "Error", logRequest.User_Id, logRequest.Room_Id, description);
                await _logRepository.AddLogAsync(errorLog);
                return errorLog;
            }

            var addedLog = await _logRepository.AddLogAsync(log);

            return addedLog;
        }

        public async Task<List<Log>> SearchLogsByUserAsync(string searchTerm)
        {
            return await _logRepository.SearchLogsByUserIdAsync(searchTerm);
        }

        public async Task<List<Log>> SearchLogsByRoomAsync(string searchTerm)
        {
            return await _logRepository.SearchLogsByRoomIdAsync(searchTerm);
        }

        public async Task<List<Log>> SearchLogsByKeycardIdAsync(string searchTerm)
        {
            return await _logRepository.SearchLogsByKeycardIdAsync(searchTerm);
        }


        public async Task<int> CountLogsAsync()
        {
            return await _logRepository.CountLogsAsync();
        }

        public async Task<int> CountLogsAsync(int room_id)
        {
            return await _logRepository.CountLogsAsync(room_id);
        }

        public async Task<int> CountErrorsAsync()
        {
            return await _logRepository.CountErrorsAsync();
        }
        private static async Task<bool> ValidateAccess(string userAccessLevel, string roomAccessLevel)
        {
            return await Task.Run(() =>
            {
                var accessLevels = new List<string> { "low", "medium", "high", "manager", "admin" };
                var userAccessIndex = accessLevels.IndexOf(userAccessLevel.ToLower());
                var roomAccessIndex = accessLevels.IndexOf(roomAccessLevel.ToLower());

                return userAccessIndex >= roomAccessIndex;
            });
        }
    }
}

