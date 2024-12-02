using CustomerRegisterationFlow.Application.Responses;
using MediatR;



namespace CustomerRegisterationFlow.Application.Features.Customers.Queries.CheckExistPhoneRequest
{
    public class CheckExistPhoneRequest : IRequest<IBaseResponse>
    {
        public string Phone { get; set; }
    }
}
