

using AutoMapper;
using AutoWrapper.Wrappers;
using CustomerRegisterationFlow.Application.Contracts.Infrastructure;
using CustomerRegisterationFlow.Application.Contracts.Presistence;
using CustomerRegisterationFlow.Application.DTOs.Customers;
using CustomerRegisterationFlow.Application.DTOs.Customers.Validators;
using CustomerRegisterationFlow.Application.Features.Customers.Commands.CreatePIN;
using CustomerRegisterationFlow.Application.Responses;
using CustomerRegisterationFlow.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;

namespace CustomerRegisterationFlow.Application.Features.Customers.Commands.CreatePin
{
    public class CreatePinCommandHandler : IRequestHandler<CreatePINCommand, IBaseResponse>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _ipasswordHasher;
        private readonly ILoggerManager _loggerManager;
        public CreatePinCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IPasswordHasher ipasswordHasher,ILoggerManager loggerManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _ipasswordHasher = ipasswordHasher;
            _loggerManager = loggerManager;
        }

        public async Task<IBaseResponse> Handle(CreatePINCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreatePinDtoValidator();
            var validationResult = validator.Validate(request.CreatePINDto);
            if (request.CreatePINDto != null && validationResult.IsValid)
            {
                var customer = await _unitOfWork.CustomerRepository.FindAsync(request.CreatePINDto.Id);
                var _saltArray = RandomNumberGenerator.GetBytes(_ipasswordHasher.keySize);
                var saltString = Convert.ToHexString(_saltArray);
                var hashedPinCode = _ipasswordHasher.HashPasword(request.CreatePINDto.PINCode, _saltArray);
                customer.PasswordHash = hashedPinCode;
                customer.Salt = saltString;
                await _unitOfWork.CustomerRepository.Update(customer);
                await _unitOfWork.SaveAsync();
                return new BaseCommandResponse()
                {
                    Message = $"PIN Code Updated",
                    Payload = _mapper.Map<CustomerBasicInfoDto>(customer),
                    Code = StatusCodes.Status204NoContent,
                    Id = customer.Id

                };
            }
            else
            {
                _loggerManager.LogError($"PIN Code Creation Failed");
                return new ErrorDetails
                {
                    Code = StatusCodes.Status304NotModified,
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList(),
                    Message = $"PIN Code Creation Failed",
                    RequestId = Guid.NewGuid(),
                };
            }
        }
    }
}
