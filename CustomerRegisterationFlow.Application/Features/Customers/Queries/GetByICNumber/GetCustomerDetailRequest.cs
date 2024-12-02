using CustomerRegisterationFlow.Application.DTOs.Customers;
using CustomerRegisterationFlow.Application.Responses;
using MediatR;


namespace CustomerRegisterationFlow.Application.Features.Customers.Queries.Get
{
    public class GetCustomerDetailRequest : IRequest<IBaseResponse>
    {
        public BaseCustomerDto BaseCustomerDto { get; set; }
    }
}
