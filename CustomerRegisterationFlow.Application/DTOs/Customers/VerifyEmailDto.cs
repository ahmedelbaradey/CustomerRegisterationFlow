using CustomerRegisterationFlow.Application.DTOs.Common;

namespace CustomerRegisterationFlow.Application.DTOs.Customers
{
    public record VerifyEmailDto : BaseDto
    {
        public string TOTP { get; set; }
    }
}
