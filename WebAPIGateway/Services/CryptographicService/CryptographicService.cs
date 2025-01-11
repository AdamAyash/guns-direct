using System.Security.Cryptography;
using System.Text;

namespace WebAPIGateway.Services.CryptographicService
{
    public class CryptographicService : ICryptographicService
    {
        private readonly int _saltSize = 16; 

        public byte[] GenerateSalt()
        {
            using (var generator = RandomNumberGenerator.Create())
            {
                var salt = new byte[_saltSize];
                generator.GetBytes(salt);
                return salt;
            }
        }

        public string HashPassword(string password, byte[] salt)
        {
            using (var sha256 = new SHA256Managed())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] saltedPassword = new byte[passwordBytes.Length + salt.Length];

                Buffer.BlockCopy(passwordBytes, 0, saltedPassword, 0, passwordBytes.Length);
                Buffer.BlockCopy(salt, 0, saltedPassword, passwordBytes.Length, salt.Length);

                byte[] hashedBytes = sha256.ComputeHash(saltedPassword);

                byte[] hashedPasswordWithSalt = new byte[hashedBytes.Length + salt.Length];
                Buffer.BlockCopy(salt, 0, hashedPasswordWithSalt, 0, salt.Length);
                Buffer.BlockCopy(hashedBytes, 0, hashedPasswordWithSalt, salt.Length, hashedBytes.Length);

                return Convert.ToBase64String(hashedPasswordWithSalt);
            }
        }
    }
}
