using CustomerRegisterationFlow.Application.DTOs.Common;
using CustomerRegisterationFlow.Application.DTOs.Customers;
using CustomerRegisterationFlow.Application.Responses;
using MediatR;


namespace CustomerRegisterationFlow.Application.Features.Customers.Queries.Get
{
    public class GetCustomerDetailByIdRequest : IRequest<IBaseResponse>
    {
        public int Id { get; set; }
    }
}
