using FluentValidation;
using CustomerRegisterationFlow.Application.Contracts.Presistence;
using CustomerRegisterationFlow.Resources;
using Microsoft.Extensions.Localization;


namespace CustomerRegisterationFlow.Application.DTOs.Customers.Validators
{
    public class CreatePinDtoValidator : AbstractValidator<CreatePINDto>
    {
        private readonly IStringLocalizer<SharedResources> _localizer;
        public CreatePinDtoValidator(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
            Include(new BaseDtoValidator(_localizer));
            RuleFor(c => c.PINCode)
              .NotEmpty().WithMessage(_localizer[SharedResourcesKey.EmptyPINCodeValidation])
              .NotNull().WithMessage(_localizer[SharedResourcesKey.EmptyPINCodeValidation])
              .MaximumLength(6).WithMessage(_localizer[SharedResourcesKey.MaximumDigitsPINCodeValidation])
              .MinimumLength(6).WithMessage(_localizer[SharedResourcesKey.MinimumDigitsPINCodeValidation]);
        }
    }
}
