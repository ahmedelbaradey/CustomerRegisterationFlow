using FluentValidation;


namespace CustomerRegisterationFlow.Application.DTOs.Customers.Validators
{

    public class HasAcceptedTermsDtoValidator : AbstractValidator<HasAcceptedTermsDto>
    {
        public HasAcceptedTermsDtoValidator()
        {
            Include(new BaseDtoValidator());
            RuleFor(c => c.HasAcceptedTerms)
                .Must(c => c).WithMessage("Must Accept Terms");
        }
    }
}
