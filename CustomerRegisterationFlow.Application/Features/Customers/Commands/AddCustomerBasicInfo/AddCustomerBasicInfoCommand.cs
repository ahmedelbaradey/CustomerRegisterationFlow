using CustomerRegisterationFlow.Application.DTOs.Customers;
using CustomerRegisterationFlow.Application.Responses;
using MediatR;


namespace CustomerRegisterationFlow.Application.Features.Customers.Commands.AddCustomerBasicInfo
{
    public class AddCustomerBasicInfoCommand : IRequest<IBaseResponse>
    {
        public CustomerBasicInfoDto CustomerBasicInfoDto { get; set; }
    }
}
