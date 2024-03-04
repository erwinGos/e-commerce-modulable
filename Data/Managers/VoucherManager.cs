using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Managers
{
    public class VoucherManager
    {
        public static string GenerateVoucher()
        {
            var guid = Guid.NewGuid();

            var base64Guid = Convert.ToBase64String(guid.ToByteArray());

            var sanitized = base64Guid
                .Replace("+", "")
                .Replace("/", "")
                .Replace("=", "");

            var neededLength = 24;
            var padded = sanitized.Length >= neededLength ? sanitized.Substring(0, neededLength) : sanitized + GetRandomString(neededLength - sanitized.Length);

            return string.Join("-", Enumerable.Range(0, 5).Select(i => padded.Substring(i * 4, 4)));
        }

        private static string GetRandomString(int length)
        {
            var random = new Random();
            var characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(characters, length)
                              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
