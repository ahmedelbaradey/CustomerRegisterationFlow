using FluentValidation;
using CustomerRegisterationFlow.Application.Contracts.Presistence;


namespace CustomerRegisterationFlow.Application.DTOs.Customers.Validators
{
    public class CustomerBasicInfoDtoValidator : AbstractValidator<CustomerBasicInfoDto>
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerBasicInfoDtoValidator(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
            Include(new BaseCustomerDtoValidator(_customerRepository));
            RuleFor(c => c.Name)
               .NotEmpty().WithMessage("{PropertyName} is Required")
               .NotNull().WithMessage("{PropertyName} is Required")
               .MaximumLength(50).WithMessage("{PropertyName} cannot exceed {ComparisonValue}");

            RuleFor(c => c.Phone)
              .NotEmpty().WithMessage("{PropertyName} is Required")
              .NotNull().WithMessage("{PropertyName} is Required")
              .MustAsync(async (phone, token) =>
              {
                  var _phoneExist = await _customerRepository.PhoneExists(phone);
                  return !_phoneExist;
              }).WithMessage("{PropertyName} is exist");

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("{PropertyName} is Required")
                .NotNull().WithMessage("{PropertyName} is Required")
                .EmailAddress().WithMessage("A valid {PropertyName} is required")
                .MustAsync(async (email, token) =>
                {
                    var _emailExist = await _customerRepository.EmailExists(email);
                    return !_emailExist;
                }).WithMessage("{PropertyName} is exist");
        }
    }
}
