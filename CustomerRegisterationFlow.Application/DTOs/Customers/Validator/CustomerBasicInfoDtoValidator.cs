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
            Include(new BaseCustomerDtoValidator(_customerRepository,_localizer));
            RuleFor(c => c.Name)
               .NotEmpty().WithMessage(_localizer[SharedResourcesKey.EmptyCustomerNameValidtion])
               .NotNull().WithMessage(_localizer[SharedResourcesKey.EmptyCustomerNameValidtion])
               .MaximumLength(50).WithMessage(_localizer[SharedResourcesKey.MaximumCharsCustomerNameValidtion]);

            RuleFor(c => c.Phone)
              .NotEmpty().WithMessage(_localizer[SharedResourcesKey.EmptyCustomerPhoneValidation])
              .NotNull().WithMessage(_localizer[SharedResourcesKey.EmptyCustomerPhoneValidation])
              .MustAsync(async (phone, token) =>
              {
                  var _phoneExist = await _customerRepository.PhoneExists(phone);
                  return !_phoneExist;
              }).WithMessage(_localizer[SharedResourcesKey.ExistCustomerPhoneValidation]);

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKey.EmptyCustomerEmailValidation])
                .NotNull().WithMessage(_localizer[SharedResourcesKey.EmptyCustomerEmailValidation])
                .EmailAddress().WithMessage(_localizer[SharedResourcesKey.ValidEmailValidation])
                .MustAsync(async (email, token) =>
                {
                    var _emailExist = await _customerRepository.EmailExists(email);
                    return !_emailExist;
                }).WithMessage(_localizer[SharedResourcesKey.ExistCustomerEmailValidation]);
        }
    }
}
