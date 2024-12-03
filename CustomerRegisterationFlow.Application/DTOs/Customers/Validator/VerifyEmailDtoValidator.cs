﻿using FluentValidation;
using CustomerRegisterationFlow.Application.Contracts.Presistence;
using CustomerRegisterationFlow.Application.Contracts.Infrastructure;
using CustomerRegisterationFlow.Resources;
using Microsoft.Extensions.Localization;

namespace CustomerRegisterationFlow.Application.DTOs.Customers.Validators
{
    public class VerifyEmailDtoValidator : AbstractValidator<VerifyEmailDto>
    {
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly ITOTP _iTOTP;
        public VerifyEmailDtoValidator(ITOTP iTOTP, IStringLocalizer<SharedResources> localizer)
        {
            _iTOTP = iTOTP;
            _localizer = localizer;
            Include(new BaseDtoValidator(_localizer));

            RuleFor(c => c.TOTP)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKey.  EmptyTOTPValidation])
                .NotNull().WithMessage(_localizer[SharedResourcesKey.EmptyTOTPValidation])
                .MaximumLength(4).WithMessage(_localizer[SharedResourcesKey.MaximumDigitsTOTPValidation])
                .MinimumLength(4).WithMessage(_localizer[SharedResourcesKey.MinimumDigitsTOTPValidation])
                .Must(ValidateTOTP).WithMessage(_localizer[SharedResourcesKey.NotValidOrExpiredTOTPValidation]);
            _localizer = localizer; 
        }
        private bool ValidateTOTP(VerifyEmailDto verifyEmail, string _TOTP)
        {
            return _iTOTP.ValidateTOTP(_otpForPhone: false, _TOTP);
        }
    }
}
