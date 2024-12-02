﻿
using CustomerRegisterationFlow.Application.Contracts.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace CustomerRegisterationFlow.Infrastructure.OTP
{
    public  class TOTP: ITOTP
    {
        public const long T0 = 0;
        public const long X = 120;
        public const int DigitLength = 4;
        private const string Key64_Phone = "31323334353637383930313233343536373839303132333435363738393031323334353637383930" +
                                      "313233343536373839303132333435363738393031323334";
        private const string Key64_Email = "1234567890123456789012345678901234567890123456789012345678901234";
        public string SendTOTP(bool _otpForPhone)
        {
            var date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0, DateTimeKind.Utc);
            var timestamp = new DateTimeOffset(date, TimeSpan.Zero).ToUnixTimeSeconds();
            long T = (timestamp - T0) / X;
            // convert to hex and pad with 16 0s
            byte[] payload = T.ToString("X016").ToBytes();
            byte[] hash =HMACAlgo.Sha512.ComputeHash(Encoding.UTF8.GetBytes(_otpForPhone ? Key64_Phone : Key64_Email), payload);
            int offset = hash[^1] & 0xf;
            int binary =
                ((hash[offset] & 0x7f) << 24)
                | ((hash[offset + 1] & 0xff) << 16)
                | ((hash[offset + 2] & 0xff) << 8)
                | (hash[offset + 3] & 0xff);
            int otp = binary % (int)Math.Pow(10, DigitLength);
            return otp.ToString($"D{DigitLength}");
        }
        public bool ValidateTOTP(bool _otpForPhone , string actualTOTP)
        {
            var date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0, DateTimeKind.Utc);
            var timestamp = new DateTimeOffset(date, TimeSpan.Zero).ToUnixTimeSeconds();
            long T = (timestamp - T0) / X;
            // convert to hex and pad with 16 0s
            byte[] payload = T.ToString("X016").ToBytes();
            byte[] hash = HMACAlgo.Sha512.ComputeHash(Encoding.UTF8.GetBytes(_otpForPhone ? Key64_Phone : Key64_Email), payload);
            int offset = hash[^1] & 0xf;
            int binary =
                ((hash[offset] & 0x7f) << 24)
                | ((hash[offset + 1] & 0xff) << 16)
                | ((hash[offset + 2] & 0xff) << 8)
                | (hash[offset + 3] & 0xff);
            int otp = binary % (int)Math.Pow(10, DigitLength);
            return otp.ToString($"D{DigitLength}") == actualTOTP;
        }
     
    }
}
