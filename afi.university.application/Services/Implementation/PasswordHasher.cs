using afi.university.application.Services.Interfaces;
using System.Security.Cryptography;

namespace afi.university.application.Services.Implementation
{
    internal sealed class PasswordHasher : IPasswordHasher
    {
        private const int _saltSize = 16;
        private const int _hashSize = 32;
        private const int _iterations = 100000;
        public static readonly HashAlgorithmName _algorithm = HashAlgorithmName.SHA512;

        public string HashPassword(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(_saltSize);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, _iterations, _algorithm, _hashSize);

            return $"{Convert.ToHexString(hash)}~{Convert.ToHexString(salt)}";
        }

        public bool VerifyPassword(string password, string dbpassword)
        {
            string[] passwordParts = dbpassword.Split('~');
            byte[] hash = Convert.FromHexString(passwordParts[0]);
            byte[] salt = Convert.FromHexString(passwordParts[1]);

            byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, _iterations, _algorithm, _hashSize);

            return hash.SequenceEqual(inputHash);
        }
    }
}
