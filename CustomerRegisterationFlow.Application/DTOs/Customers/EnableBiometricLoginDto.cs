using CustomerRegisterationFlow.Application.DTOs.Common;


namespace CustomerRegisterationFlow.Application.DTOs.Customers
{
    public record EnableBiometricLoginDto: BaseDto
    {
        public bool IsBiometricLoginEnabled { get; set; }
    }
}
