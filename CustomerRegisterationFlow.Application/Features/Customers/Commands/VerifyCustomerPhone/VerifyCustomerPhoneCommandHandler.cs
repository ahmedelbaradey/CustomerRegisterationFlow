using AutoMapper;
using MediatR;
using CustomerRegisterationFlow.Application.Contracts.Presistence;
using CustomerRegisterationFlow.Application.Responses;
using CustomerRegisterationFlow.Application.Features.Customers.Commands.VerifyCustomerPhone;
using CustomerRegisterationFlow.Application.DTOs.Customers.Validators;
using CustomerRegisterationFlow.Application.Contracts.Infrastructure;
using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Http;
using CustomerRegisterationFlow.Resources;
using Microsoft.Extensions.Localization;

namespace CustomerRegisterationFlow.Application.Features.Customers.Commands.AddCustomerBasicInfo
{
    public class VerifyCustomerPhoneCommandHandler : IRequestHandler<VerifyCustomerPhoneCommand, IBaseResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ITOTP _iTOTP;
        private readonly ILoggerManager _loggerManager;
        private readonly IStringLocalizer<SharedResources> _localizer;
        public VerifyCustomerPhoneCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ITOTP iTOTP, ILoggerManager loggerManager, IStringLocalizer<SharedResources> localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _iTOTP = iTOTP;
            _loggerManager = loggerManager;
            _localizer = localizer; 
        }

        public async Task<IBaseResponse> Handle(VerifyCustomerPhoneCommand request, CancellationToken cancellationToken)
        {
            var validator = new VerifyPhoneDtoValidator(_iTOTP, _localizer);
            var validationResult = validator.Validate(request.VerifyPhoneDto);
            if (request.VerifyPhoneDto != null && validationResult.IsValid)
            {
                return new BaseCommandResponse()
                {
                    Message = $"{_localizer[SharedResourcesKey.PhoneVerifiedAndEmailTOTPIs]} {_iTOTP.SendTOTP(_otpForPhone: false)} {_localizer[SharedResourcesKey.TOTPExpireAfter]}",
                    Code = StatusCodes.Status204NoContent,
                    Id = request.VerifyPhoneDto.Id
                };
            }
            else
            {
                _loggerManager.LogError($"{_localizer[SharedResourcesKey.PhoneVerificationFailed]}");
                return new ErrorDetails
                {
                    Code = StatusCodes.Status304NotModified,
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList(),
                    Message = $"{_localizer[SharedResourcesKey.PhoneVerificationFailed]}",
                    RequestId = Guid.NewGuid(),
                };
            }
        }
    }
}
