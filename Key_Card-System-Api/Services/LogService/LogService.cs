using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

            var log = new Log(0, logRequest.Entry_Type, logRequest.User_Id, logRequest.Room_Id, description);
            log.User = user;
            log.Room = room;

            if (!accessGranted)
            {
                var errorLog = new Log(0, "Error", logRequest.User_Id, logRequest.Room_Id, "Access level does not match.");
                await _logRepository.AddLogAsync(errorLog);
                return errorLog;
            }

            var addedLog = await _logRepository.AddLogAsync(log);

            return addedLog;
        }

        public async Task<List<Log>> SearchLogsAsync(string searchTerm)
        {
            return await _logRepository.SearchLogsAsync(searchTerm);
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

