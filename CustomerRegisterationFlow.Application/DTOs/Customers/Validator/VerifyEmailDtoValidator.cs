using FluentValidation;
using CustomerRegisterationFlow.Application.Contracts.Presistence;
using CustomerRegisterationFlow.Application.Contracts.Infrastructure;

namespace CustomerRegisterationFlow.Application.DTOs.Customers.Validators
{

    public class VerifyEmailDtoValidator : AbstractValidator<VerifyEmailDto>
    {
        private readonly ITOTP _iTOTP;
        public VerifyEmailDtoValidator(ITOTP iTOTP)
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
        private bool ValidateTOTP(VerifyEmailDto verifyEmail, string _TOTP)
        {
            return _iTOTP.ValidateTOTP(_otpForPhone: false, _TOTP);
        }
    }
}
