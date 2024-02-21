﻿using Keycard_System_API.Models;

namespace Key_Card_System_Api.Services.UserService
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task CreateUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(int id);
        Task<bool> DeactivateUserAsync(int id);
        Task<User?> AuthenticateByUsernameAsync(string username, string password);
        Task<User?> AuthenticateByEmailAsync(string email, string password);
        Task<User?> RegisterAsync(User user, string password);
        Task<List<User>> SearchUsersAsync(string keyId);
    }
}
