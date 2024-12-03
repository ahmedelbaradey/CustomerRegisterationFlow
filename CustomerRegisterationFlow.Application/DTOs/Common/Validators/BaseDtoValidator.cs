using FluentValidation;
using CustomerRegisterationFlow.Application.Contracts.Presistence;
using CustomerRegisterationFlow.Application.DTOs.Common;
using CustomerRegisterationFlow.Resources;
using Microsoft.Extensions.Localization;


namespace CustomerRegisterationFlow.Application.DTOs.Customers.Validators
{
    public class BaseDtoValidator : AbstractValidator<BaseDto>
    {
        private readonly IStringLocalizer<SharedResources> _localizer;
        public BaseDtoValidator(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKey.EmptyIdValidation]);
        }
    }
}
