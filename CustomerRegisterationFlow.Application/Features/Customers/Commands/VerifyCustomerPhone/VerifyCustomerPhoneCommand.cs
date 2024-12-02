using CustomerRegisterationFlow.Application.DTOs.Customers;
using CustomerRegisterationFlow.Application.Responses;
using MediatR;


namespace CustomerRegisterationFlow.Application.Features.Customers.Commands.VerifyCustomerPhone
{
    public class VerifyCustomerPhoneCommand : IRequest<IBaseResponse>
    {
        public VerifyPhoneDto VerifyPhoneDto { get; set; }
    }
}
