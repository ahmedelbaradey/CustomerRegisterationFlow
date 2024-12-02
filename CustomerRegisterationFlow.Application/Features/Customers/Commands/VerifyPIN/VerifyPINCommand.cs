using CustomerRegisterationFlow.Application.DTOs.Customers;
using CustomerRegisterationFlow.Application.Responses;
using MediatR;


namespace CustomerRegisterationFlow.Application.Features.Customers.Commands.VerifyPIN
{ 
    public class VerifyPINCommand : IRequest<IBaseResponse>
    {
        public VerifyPINDto VerifyPINDto { get; set; }
    }
}
