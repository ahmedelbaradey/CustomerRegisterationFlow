using CustomerRegisterationFlow.Resources;
using FluentValidation;
using Microsoft.Extensions.Localization;


namespace CustomerRegisterationFlow.Application.DTOs.Customers.Validators
{

    public class EnableBiometricLoginDtoValidator : AbstractValidator<EnableBiometricLoginDto>
    {
        private readonly IStringLocalizer<SharedResources> _localizer;
        public EnableBiometricLoginDtoValidator(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer; 
            Include(new BaseDtoValidator(_localizer));
        }
    }
}
