﻿using System.Security.Cryptography;
using System.Text;

namespace Keycard_System_API.Utils
{
    public static class PasswordHash
    {
        public static string HashPassword(string password)
        {
            var hashedBytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return hashedPassword.Equals(HashPassword(password), StringComparison.OrdinalIgnoreCase);
        }
    }
}
