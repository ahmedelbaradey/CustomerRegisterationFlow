using FluentValidation;
using CustomerRegisterationFlow.Application.Contracts.Presistence;

namespace CustomerRegisterationFlow.Application.DTOs.Customers.Validators
{

    public class BaseCustomerDtoValidator : AbstractValidator<BaseCustomerDto>
    {
        private readonly ICustomerRepository _customerRepository;
        public BaseCustomerDtoValidator(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;

            RuleFor(c => c.ICNumber)
              .NotEmpty().WithMessage("{PropertyName} is Required")
              .NotNull().WithMessage("{PropertyName} is Required")
              .MustAsync(async (icNumber, token) =>
              {
                  var _icNumberExist = await _customerRepository.ICNumberExists(icNumber);
                  return !_icNumberExist;
              }).WithMessage("{PropertyName} is exist");
        }
    }
}
