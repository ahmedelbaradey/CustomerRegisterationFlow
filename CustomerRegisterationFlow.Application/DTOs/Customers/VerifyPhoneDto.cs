using CustomerRegisterationFlow.Application.DTOs.Common;

namespace CustomerRegisterationFlow.Application.DTOs.Customers
{
    public record VerifyPhoneDto : BaseDto
    {
        public string TOTP { get; set; }
    }
}
