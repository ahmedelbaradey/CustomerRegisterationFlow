using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CustomerRegisterationFlow.Application.Contracts.Infrastructure
{
    public interface IPasswordHasher
    {
        string HashPasword(string password, byte[] salt);
        bool VerifyPassword(string password, string hash, byte[] salt);
        public int keySize { get; set; }
    }
}
