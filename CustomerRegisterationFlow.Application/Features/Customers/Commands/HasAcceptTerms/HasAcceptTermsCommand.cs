using CustomerRegisterationFlow.Application.DTOs.Customers;
using CustomerRegisterationFlow.Application.Responses;
using MediatR;


namespace CustomerRegisterationFlow.Application.Features.LeaveRequests.Commands.HasAcceptTerms
{
    public class HasAcceptTermsCommand : IRequest<IBaseResponse>
    {
        public HasAcceptedTermsDto HasAcceptedTermsDto { get; set; }
    }
}
