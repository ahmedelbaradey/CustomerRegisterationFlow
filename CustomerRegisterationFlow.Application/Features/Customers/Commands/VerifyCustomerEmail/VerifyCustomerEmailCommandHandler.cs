using AutoMapper;
using MediatR;
using CustomerRegisterationFlow.Application.Contracts.Presistence;
using CustomerRegisterationFlow.Application.Responses;
using CustomerRegisterationFlow.Application.DTOs.Customers.Validators;
using CustomerRegisterationFlow.Application.Contracts.Infrastructure;
using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Http;
using CustomerRegisterationFlow.Domain.Entities;
using CustomerRegisterationFlow.Application.DTOs.Customers;
using CustomerRegisterationFlow.Resources;
using Microsoft.Extensions.Localization;

namespace CustomerRegisterationFlow.Application.Features.Customers.Commands.VerifyCustomerEmail
{
    public class VerifyCustomerEmailCommandHandler : IRequestHandler<VerifyCustomerEmailCommand, IBaseResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ITOTP _iTOTP;
        private readonly ILoggerManager _loggerManager;
        private readonly IStringLocalizer<SharedResources> _localizer;
        public VerifyCustomerEmailCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ITOTP iTOTP, ILoggerManager loggerManager, IStringLocalizer<SharedResources> localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _iTOTP = iTOTP;
            _loggerManager = loggerManager;
            _localizer = localizer;
        }
        public async Task<IBaseResponse> Handle(VerifyCustomerEmailCommand request, CancellationToken cancellationToken)
        {
            var validator = new VerifyEmailDtoValidator(_iTOTP,_localizer);
            var validationResult = validator.Validate(request.VerifyEmailDto);
            if (request.VerifyEmailDto != null && validationResult.IsValid)
            {
                return new BaseCommandResponse()
                {
                    Message = $"{_localizer[SharedResourcesKey.EmailVerirfiedSucessfully]}",
                    Code = StatusCodes.Status204NoContent,
                    Id = request.VerifyEmailDto.Id
                };
            }
            else
            {
                _loggerManager.LogError($"{_localizer[SharedResourcesKey.EmailVerificationFailed]}");
                return new ErrorDetails
                {
                    Code = StatusCodes.Status304NotModified,
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList(),
                    Message = $"{_localizer[SharedResourcesKey.EmailVerificationFailed]}",
                    RequestId = Guid.NewGuid(),
                };
            }

        }
    }
}
