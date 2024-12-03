using FluentValidation;
using CustomerRegisterationFlow.Application.Contracts.Presistence;
using CustomerRegisterationFlow.Application.Contracts.Infrastructure;
using System.Text;
using CustomerRegisterationFlow.Resources;
using Microsoft.Extensions.Localization;

namespace CustomerRegisterationFlow.Application.DTOs.Customers.Validators
{
    public class VerifyPhoneDtoValidator : AbstractValidator<VerifyPhoneDto>
    {
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly ITOTP _iTOTP;
        public VerifyPhoneDtoValidator(ITOTP iTOTP, IStringLocalizer<SharedResources> localizer)
        {
            _iTOTP = iTOTP;
            _localizer = localizer;
            Include(new BaseDtoValidator(_localizer));
            RuleFor(c => c.TOTP)
              .NotEmpty().WithMessage(_localizer[SharedResourcesKey.EmptyTOTPValidation])
              .NotNull().WithMessage(_localizer[SharedResourcesKey.EmptyTOTPValidation])
              .MaximumLength(4).WithMessage(_localizer[SharedResourcesKey.MaximumDigitsTOTPValidation])
              .MinimumLength(4).WithMessage(_localizer[SharedResourcesKey.MinimumDigitsTOTPValidation])
              .Must(ValidateTOTP).WithMessage(_localizer[SharedResourcesKey.NotValidOrExpiredTOTPValidation]);
        }
        private bool ValidateTOTP(VerifyPhoneDto verifyPhoneDto, string _TOTP)
        {
            return _iTOTP.ValidateTOTP(_otpForPhone: true, _TOTP);
        }
    }
}
