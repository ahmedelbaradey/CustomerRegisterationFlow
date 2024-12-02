using FluentValidation;
using CustomerRegisterationFlow.Application.Contracts.Presistence;
using CustomerRegisterationFlow.Application.Contracts.Infrastructure;
using System.Text;

namespace CustomerRegisterationFlow.Application.DTOs.Customers.Validators
{
    public class VerifyPhoneDtoValidator : AbstractValidator<VerifyPhoneDto>
    {
        private readonly ITOTP _iTOTP;
        public VerifyPhoneDtoValidator(ITOTP iTOTP)
        {
            _iTOTP = iTOTP;

            Include(new BaseDtoValidator());
            RuleFor(c => c.TOTP)
              .NotEmpty().WithMessage("{PropertyName} is Required")
              .NotNull().WithMessage("{PropertyName} is Required")
              .MaximumLength(4).WithMessage("{PropertyName} must be 6 digits")
              .MinimumLength(4).WithMessage("{PropertyName}  must be 6 digits")
              .Must(ValidateTOTP).WithMessage("{PropertyName} not valid or expired");
        }
        private bool ValidateTOTP(VerifyPhoneDto verifyPhoneDto, string _TOTP)
        {
            return _iTOTP.ValidateTOTP(_otpForPhone: true, _TOTP);
        }
    }
}
