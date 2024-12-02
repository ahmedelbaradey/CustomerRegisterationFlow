using FluentValidation;


namespace CustomerRegisterationFlow.Application.DTOs.Customers.Validators
{

    public class EnableBiometricLoginDtoValidator : AbstractValidator<EnableBiometricLoginDto>
    {
        public EnableBiometricLoginDtoValidator()
        {
            Include(new BaseDtoValidator());
        }
    }
}
