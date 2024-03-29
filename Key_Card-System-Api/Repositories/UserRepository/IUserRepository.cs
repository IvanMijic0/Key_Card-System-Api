﻿using Keycard_System_API.Models;

namespace Key_Card_System_Api.Repositories.UserRepository
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task CreateUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
        Task UpdateUsersKeyCardAsync(int user_id, string response, string access_level);
        Task<bool> DeleteUserAsync(int id);
        Task<User?> GetUserByEmailAsync(string email);
        Task<User?> GetUserByUsernameAsync(string username);
        Task<List<User>> SearchUsersByUsernameAsync(string searchTerm);
        Task<List<User>> SearchUsersByKeyIdAsync(string searchTerm);
        Task UpdateUsersKeyCardAcessLevelAsync(int user_id, string access_level);
    }
}
