using CustomerRegisterationFlow.Application.Responses;
using MediatR;


namespace CustomerRegisterationFlow.Application.Features.Customers.Queries.CheckExistICNumberRequest
{
    public class CheckExistICNumberRequest : IRequest<IBaseResponse>
    {
        public int? ICNumber { get; set; }
    }
}
