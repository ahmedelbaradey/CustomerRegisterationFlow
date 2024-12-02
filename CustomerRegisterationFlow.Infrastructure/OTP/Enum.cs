using CustomerRegisterationFlow.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace CustomerRegisterationFlow.Infrastructure.OTP
{
    public enum HMACAlgo
    {
        Sha1,
        Sha256,
        Sha512,
    }
}
