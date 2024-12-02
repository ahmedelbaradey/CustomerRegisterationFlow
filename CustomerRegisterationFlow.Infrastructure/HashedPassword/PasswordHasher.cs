using CustomerRegisterationFlow.Application.Contracts.Infrastructure;
using System.Security.Cryptography;
using System.Text;


namespace CustomerRegisterationFlow.Infrastructure.HashedPassword
{
    public class PasswordHasher : IPasswordHasher
    {
        const int iterations = 350000;
        HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
       public  int keySize { get; set;  } = 64; 

        public string HashPasword(string password, byte[] salt)
        {
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                hashAlgorithm,
                keySize);
            return Convert.ToHexString(hash);
        }
        public bool VerifyPassword(string password, string hash, byte[] salt)
        {     
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(
              Encoding.UTF8.GetBytes(password),
              salt,
              iterations,
              hashAlgorithm,
              keySize);

            return Convert.ToHexString(hashToCompare) == hash;
        }
    }
}
