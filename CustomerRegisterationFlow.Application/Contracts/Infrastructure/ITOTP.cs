using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CustomerRegisterationFlow.Application.Contracts.Infrastructure
{
    public interface ITOTP
    {
        string SendTOTP(bool _otpForPhone);
        bool ValidateTOTP(bool _otpForPhone, string actualTOTP);
    }
}
