using CustomerRegisterationFlow.Application.DTOs.Customers;
using CustomerRegisterationFlow.Application.Responses;
using MediatR;


namespace CustomerRegisterationFlow.Application.Features.Customers.Commands.VerifyCustomerEmail
{
    public class VerifyCustomerEmailCommand : IRequest<IBaseResponse>
    {
        public VerifyEmailDto VerifyEmailDto { get; set; }
    }
}
