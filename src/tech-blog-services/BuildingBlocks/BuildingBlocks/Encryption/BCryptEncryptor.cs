using BCrypt.Net;

namespace BuildingBlocks.Encryption
{
    public static class BCryptEncryptor
    {
        public static string HashPassword(string Password)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(Password, HashType.SHA512);
        }

        public static bool VerifyPassword(string Password, string PasswordHash)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(Password, PasswordHash, HashType.SHA512);
        }
    }
}
