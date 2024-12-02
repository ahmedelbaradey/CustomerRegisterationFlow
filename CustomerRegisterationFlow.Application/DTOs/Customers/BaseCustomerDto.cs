using CustomerRegisterationFlow.Application.DTOs.Common;

namespace CustomerRegisterationFlow.Application.DTOs.Customers
{
    public record BaseCustomerDto : BaseDto
    {
        public int ICNumber { get; set; }
    }
}
