using AutoMapper;
using MediatR;
using CustomerRegisterationFlow.Application.Contracts.Presistence;
using CustomerRegisterationFlow.Application.Responses;
using CustomerRegisterationFlow.Application.DTOs.Customers.Validators;
using CustomerRegisterationFlow.Application.Contracts.Infrastructure;
using FluentValidation;
using CustomerRegisterationFlow.Application.DTOs.Customers;
using CustomerRegisterationFlow.Domain.Entities;
using Microsoft.AspNetCore.Http;
using AutoWrapper.Wrappers;
using CustomerRegisterationFlow.Resources;
using Microsoft.Extensions.Localization;

namespace CustomerRegisterationFlow.Application.Features.Customers.Commands.VerifyPIN
{
    public class VerifyPINCommandHandler : IRequestHandler<VerifyPINCommand, IBaseResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _ipasswordHasher;
        private readonly ILoggerManager _loggerManager;
        private readonly IStringLocalizer<SharedResources> _localizer;
        public VerifyPINCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IPasswordHasher ipasswordHasher, ILoggerManager loggerManager , IStringLocalizer<SharedResources> localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _ipasswordHasher = ipasswordHasher;
            _loggerManager = loggerManager;
            _localizer = localizer;
        }
        public async Task<IBaseResponse> Handle(VerifyPINCommand request, CancellationToken cancellationToken)
        {
            var validator = new VerifyPINDtoValidator(_unitOfWork.CustomerRepository, _ipasswordHasher,_localizer);
            var validationResult = validator.Validate(request.VerifyPINDto);
            var response = new BaseCommandResponse();
            if (request.VerifyPINDto != null && validationResult.IsValid)
            {
                return new BaseCommandResponse()
                {
                    Message = $"{_localizer[SharedResourcesKey.PINCodeVerirfiedSucessfully]}",
                    Code = StatusCodes.Status204NoContent,
                    Id = request.VerifyPINDto.Id
                };
            }
            else
            {
                _loggerManager.LogError($"{_localizer[SharedResourcesKey.PINCodeVerificationFailed]}");
                return new ErrorDetails()
                {
                    Code = StatusCodes.Status304NotModified,
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList(),
                    Message = $"{_localizer[SharedResourcesKey.PINCodeVerificationFailed]}",
                    RequestId = Guid.NewGuid(),
                };
            }
        }
    }
}
