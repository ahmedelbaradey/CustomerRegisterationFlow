using CustomerRegisterationFlow.Application.DTOs.Customers;
using CustomerRegisterationFlow.Application.Responses;
using MediatR;


namespace CustomerRegisterationFlow.Application.Features.Customers.Commands.EnableBiometricLogin
{
    public class EnableBiometricLoginCommand : IRequest<IBaseResponse>
    {
        public EnableBiometricLoginDto EnableBiometricLoginDto { get; set; }
    }
}
