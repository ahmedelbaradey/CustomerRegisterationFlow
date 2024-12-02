using CustomerRegisterationFlow.Application.DTOs.Common;


namespace CustomerRegisterationFlow.Application.DTOs.Customers
{
    public record CustomerBasicInfoDto : BaseCustomerDto
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
