using FluentValidation;
using CustomerRegisterationFlow.Application.Contracts.Presistence;


namespace CustomerRegisterationFlow.Application.DTOs.Customers.Validators
{
    public class CreatePinDtoValidator : AbstractValidator<CreatePINDto>
    {
     
        public CreatePinDtoValidator()
        {
            Include(new BaseDtoValidator());
            RuleFor(c => c.PINCode)
              .NotEmpty().WithMessage("{PropertyName} is Required")
              .NotNull().WithMessage("{PropertyName} is Required")
              .MaximumLength(6).WithMessage("{PropertyName} must be 6 digits")
              .MinimumLength(6).WithMessage("{PropertyName}  must be 6 digits");
        }
    }
}
