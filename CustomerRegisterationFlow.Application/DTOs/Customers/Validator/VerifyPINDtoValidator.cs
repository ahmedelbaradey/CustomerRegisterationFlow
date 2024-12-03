using FluentValidation;
using CustomerRegisterationFlow.Application.Contracts.Presistence;
using CustomerRegisterationFlow.Application.Contracts.Infrastructure;
using System.Text;
using CustomerRegisterationFlow.Resources;
using Microsoft.Extensions.Localization;


namespace CustomerRegisterationFlow.Application.DTOs.Customers.Validators
{
    public class VerifyPINDtoValidator : AbstractValidator<VerifyPINDto>
    {

        private readonly ICustomerRepository _customerRepository;
        private readonly IPasswordHasher _ipasswordHasher;
        private readonly IStringLocalizer<SharedResources> _localizer;
        public VerifyPINDtoValidator(ICustomerRepository customerRepository, IPasswordHasher ipasswordHasher, IStringLocalizer<SharedResources> localizer)
        {
            _customerRepository = customerRepository;
            _ipasswordHasher = ipasswordHasher;
            _localizer = localizer; 
            Include(new BaseDtoValidator(_localizer));

            RuleFor(c => c.PINCode)
              .NotEmpty().WithMessage(_localizer[SharedResourcesKey.EmptyPINCodeValidation])
              .NotNull().WithMessage(_localizer[SharedResourcesKey.EmptyPINCodeValidation])
              .MaximumLength(6).WithMessage(_localizer[SharedResourcesKey.MaximumDigitsPINCodeValidation])
              .MinimumLength(6).WithMessage(_localizer[SharedResourcesKey.MinimumDigitsPINCodeValidation])
              .Must(VerifyPINCode).WithMessage(_localizer[SharedResourcesKey.NotValidPINCodeValidation]);

        }
        private bool VerifyPINCode(VerifyPINDto verifyPINDto,string _PINCode)
        {
            var customer =  _customerRepository.FindAsync(verifyPINDto.Id).Result;
            return _ipasswordHasher.VerifyPassword(_PINCode, customer.PasswordHash, Convert.FromHexString(customer.Salt));
           
        }
    }
}
