using FluentValidation;
using CustomerRegisterationFlow.Application.Contracts.Presistence;
using CustomerRegisterationFlow.Resources;
using Microsoft.Extensions.Localization;

namespace CustomerRegisterationFlow.Application.DTOs.Customers.Validators
{

    public class BaseCustomerDtoValidator : AbstractValidator<BaseCustomerDto>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IStringLocalizer<SharedResources> _localizer;
        public BaseCustomerDtoValidator(ICustomerRepository customerRepository, IStringLocalizer<SharedResources> localizer)
        {
            _customerRepository = customerRepository;
            _localizer = localizer;
            RuleFor(c => c.ICNumber)
              .NotEmpty().WithMessage(_localizer[SharedResourcesKey.EmptyICNumberValidation])
              .NotNull().WithMessage(_localizer[SharedResourcesKey.EmptyICNumberValidation])
              .MustAsync(async (icNumber, token) =>
              {
                  var _icNumberExist = await _customerRepository.ICNumberExists(icNumber);
                  return !_icNumberExist;
              }).WithMessage(_localizer[SharedResourcesKey.ExistICNumberValidation]);
        }
    }
}
