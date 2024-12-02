using FluentValidation;
using CustomerRegisterationFlow.Application.Contracts.Presistence;
using CustomerRegisterationFlow.Resources;
using Microsoft.Extensions.Localization;


namespace CustomerRegisterationFlow.Application.DTOs.Customers.Validators
{
    public class CustomerBasicInfoDtoValidator : AbstractValidator<CustomerBasicInfoDto>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IStringLocalizer<SharedResources> _localizer;
        public CustomerBasicInfoDtoValidator(ICustomerRepository customerRepository, IStringLocalizer<SharedResources> localizer)
        {
            _customerRepository = customerRepository;
            _localizer = localizer;
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
                .NotEmpty().WithMessage(_localizer[SharedResourcesKey.EmptyEmailValidation])
                .NotNull().WithMessage(_localizer[SharedResourcesKey.EmptyEmailValidation])
                .EmailAddress().WithMessage("A valid {PropertyName} is required")
                .MustAsync(async (email, token) =>
                {
                    var _emailExist = await _customerRepository.EmailExists(email);
                    return !_emailExist;
                }).WithMessage("{PropertyName} is exist");
        }
    }
}
