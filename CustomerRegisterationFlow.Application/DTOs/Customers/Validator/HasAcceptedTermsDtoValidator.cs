using CustomerRegisterationFlow.Resources;
using FluentValidation;
using Microsoft.Extensions.Localization;


namespace CustomerRegisterationFlow.Application.DTOs.Customers.Validators
{

    public class HasAcceptedTermsDtoValidator : AbstractValidator<HasAcceptedTermsDto>
    {
        private readonly IStringLocalizer<SharedResources> _localizer;
        public HasAcceptedTermsDtoValidator(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
            Include(new BaseDtoValidator(_localizer));
            RuleFor(c => c.HasAcceptedTerms)
              .Must(c => c).WithMessage(_localizer[SharedResourcesKey.HasAcceptedTerms]);
        }
    }
}
