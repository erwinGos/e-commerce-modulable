

using BCrypt.Net;
using System.Net.NetworkInformation;

namespace Data.Managers
{
    public class IdentityManager
    {
        public static string HashPassword(string password)
        {
            // Generate a random salt value
            string salt = BCrypt.Net.BCrypt.GenerateSalt();

            // Hash the password using bcrypt
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);

            return hashedPassword;
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
