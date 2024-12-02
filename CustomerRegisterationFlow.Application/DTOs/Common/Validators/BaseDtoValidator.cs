using FluentValidation;
using CustomerRegisterationFlow.Application.Contracts.Presistence;
using CustomerRegisterationFlow.Application.DTOs.Common;


namespace CustomerRegisterationFlow.Application.DTOs.Customers.Validators
{
    public class BaseDtoValidator : AbstractValidator<BaseDto>
    {
        public BaseDtoValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("{PropertyName} is Required");
        }
    }
}
