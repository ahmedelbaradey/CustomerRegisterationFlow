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

namespace CustomerRegisterationFlow.Application.Features.Customers.Commands.VerifyPIN
{
    public class VerifyPINCommandHandler : IRequestHandler<VerifyPINCommand, IBaseResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _ipasswordHasher;
        private readonly ILoggerManager _loggerManager;
        public VerifyPINCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IPasswordHasher ipasswordHasher, ILoggerManager loggerManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _ipasswordHasher = ipasswordHasher;
            _loggerManager = loggerManager;
        }
        public async Task<IBaseResponse> Handle(VerifyPINCommand request, CancellationToken cancellationToken)
        {
            var validator = new VerifyPINDtoValidator(_unitOfWork.CustomerRepository, _ipasswordHasher);
            var validationResult = validator.Validate(request.VerifyPINDto);
            var response = new BaseCommandResponse();
            if (request.VerifyPINDto != null && validationResult.IsValid)
            {
                return new BaseCommandResponse()
                {
                    Message = $"PIN Code Verirfied Sucessfully",
                    Code = StatusCodes.Status204NoContent,
                    Id = request.VerifyPINDto.Id
                };
            }
            else
            {
                _loggerManager.LogError($"PIN Code Verification Failed");
                return new ErrorDetails()
                {
                    Code = StatusCodes.Status304NotModified,
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList(),
                    Message = $"PIN Code Verification Failed",
                    RequestId = Guid.NewGuid(),
                };
            }
        }
    }
}
