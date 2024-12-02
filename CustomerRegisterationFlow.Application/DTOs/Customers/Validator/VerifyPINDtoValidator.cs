using FluentValidation;
using CustomerRegisterationFlow.Application.Contracts.Presistence;
using CustomerRegisterationFlow.Application.Contracts.Infrastructure;
using System.Text;


namespace CustomerRegisterationFlow.Application.DTOs.Customers.Validators
{
    public class VerifyPINDtoValidator : AbstractValidator<VerifyPINDto>
    {

        private readonly ICustomerRepository _customerRepository;
        private readonly IPasswordHasher _ipasswordHasher;
        public VerifyPINDtoValidator(ICustomerRepository customerRepository, IPasswordHasher ipasswordHasher)
        {
            _customerRepository = customerRepository;
            _ipasswordHasher = ipasswordHasher;

            Include(new BaseDtoValidator());

            RuleFor(c => c.PINCode)
              .NotEmpty().WithMessage("{PropertyName} is Required")
              .NotNull().WithMessage("{PropertyName} is Required")
              .MaximumLength(6).WithMessage("{PropertyName} must be 6 digits")
              .MinimumLength(6).WithMessage("{PropertyName}  must be 6 digits")
              .Must(VerifyPINCode).WithMessage("{PropertyName}  not valid");

        }
        private bool VerifyPINCode(VerifyPINDto verifyPINDto,string _PINCode)
        {
            var customer =  _customerRepository.FindAsync(verifyPINDto.Id).Result;
            return _ipasswordHasher.VerifyPassword(_PINCode, customer.PasswordHash, Convert.FromHexString(customer.Salt));
           
        }
    }
}
