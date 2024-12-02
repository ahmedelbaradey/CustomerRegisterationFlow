using CustomerRegisterationFlow.Application.DTOs.Common;


namespace CustomerRegisterationFlow.Application.DTOs.Customers
{
    public record HasAcceptedTermsDto : BaseDto
    {
        public bool HasAcceptedTerms { get; set; }
    }
}
