using BlogsWebApi.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BlogsWebApi.Security
{
    public static class UserValidator
    {
        public static string GetHash(string password)
        {
            var hasher = new PasswordHasher<object>();
            string hashedPassword = hasher.HashPassword(null, password);
            return hashedPassword;
        }

        public static bool VerifyHash(User user, string password)
        {
            var hasher = new PasswordHasher<User>();
            var result = hasher.VerifyHashedPassword(user, user.Password, password);

            if (result == PasswordVerificationResult.Success)
                return true;

            return false;

        }

    }
}
