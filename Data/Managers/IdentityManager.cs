

using BCrypt.Net;

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
    }
}
