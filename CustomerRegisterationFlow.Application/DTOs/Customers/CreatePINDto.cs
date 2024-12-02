using CustomerRegisterationFlow.Application.DTOs.Common;


namespace CustomerRegisterationFlow.Application.DTOs.Customers
{
    public record CreatePINDto : BaseDto
    {
        public string PINCode { get; set; }
    }
}
