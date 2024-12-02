using CustomerRegisterationFlow.Application.Responses;
using MediatR;


namespace CustomerRegisterationFlow.Application.Features.Customers.Queries.CheckExistEmail
{
    public class CheckExistEmailRequest : IRequest<IBaseResponse>
    {
        public string Email { get; set; }
    }
}
