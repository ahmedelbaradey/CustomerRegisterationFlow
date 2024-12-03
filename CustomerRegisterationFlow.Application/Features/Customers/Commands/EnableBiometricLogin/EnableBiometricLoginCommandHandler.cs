using AutoMapper;
using MediatR;
using CustomerRegisterationFlow.Application.Contracts.Presistence;
using CustomerRegisterationFlow.Application.Responses;
using CustomerRegisterationFlow.Application.DTOs.Customers.Validators;
using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Http;
using CustomerRegisterationFlow.Resources;
using Microsoft.Extensions.Localization;

namespace CustomerRegisterationFlow.Application.Features.Customers.Commands.EnableBiometricLogin
{
    public class EnableBiometricLoginCommandHandler : IRequestHandler<EnableBiometricLoginCommand, IBaseResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        public EnableBiometricLoginCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IStringLocalizer<SharedResources> localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }
        public async Task<IBaseResponse> Handle(EnableBiometricLoginCommand request, CancellationToken cancellationToken)
        {
            var validator = new EnableBiometricLoginDtoValidator(_localizer);
            var validationResult = validator.Validate(request.EnableBiometricLoginDto);
            if (request.EnableBiometricLoginDto != null && validationResult.IsValid)
            {
                var customer = await _unitOfWork.CustomerRepository.FindAsync(request.EnableBiometricLoginDto.Id);
                customer.IsBiometricLoginEnabled = request.EnableBiometricLoginDto.IsBiometricLoginEnabled;
                await _unitOfWork.CustomerRepository.Update(customer);
                await _unitOfWork.SaveAsync();
                return new BaseCommandResponse()
                {
                    Message = $"{_localizer[SharedResourcesKey.BiometricLoginEnabledSucessfully]}",
                    Code = StatusCodes.Status204NoContent,
                    Id = customer.Id
                };
            }
            else
            {
                return new ErrorDetails
                {
                    Code = StatusCodes.Status304NotModified,
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList(),
                    Message = $"{_localizer[SharedResourcesKey.BiometricLoginEnabledFailed]}",
                    RequestId = Guid.NewGuid(),
                };
            }
        }
    }
}
