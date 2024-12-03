using AutoMapper;
using MediatR;
using CustomerRegisterationFlow.Application.Contracts.Presistence;
using CustomerRegisterationFlow.Application.Responses;
using CustomerRegisterationFlow.Application.DTOs.Customers.Validators;
using CustomerRegisterationFlow.Domain.Entities;
using CustomerRegisterationFlow.Application.Contracts.Infrastructure;
using AutoWrapper.Wrappers;
using CustomerRegisterationFlow.Application.DTOs.Customers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using CustomerRegisterationFlow.Resources;

namespace CustomerRegisterationFlow.Application.Features.Customers.Commands.AddCustomerBasicInfo
{
    public class AddCustomerBasicInfoCommandHandler : IRequestHandler<AddCustomerBasicInfoCommand, IBaseResponse>
    {
       
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ITOTP _iTOTP;
        private readonly ILoggerManager _loggerManager;
        private readonly IStringLocalizer<SharedResources> _localizer;
        public AddCustomerBasicInfoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ITOTP iTOTP, ILoggerManager loggerManager, IStringLocalizer<SharedResources> localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _iTOTP = iTOTP;
            _loggerManager = loggerManager;
            _localizer = localizer;
        }

        public async Task<IBaseResponse> Handle(AddCustomerBasicInfoCommand request, CancellationToken cancellationToken)
        {
            var validator = new CustomerBasicInfoDtoValidator(_unitOfWork.CustomerRepository, _localizer);
            var validationResult = await validator.ValidateAsync(request.CustomerBasicInfoDto);
            if (request.CustomerBasicInfoDto != null && validationResult.IsValid)
            {
                var customer = _mapper.Map<Customer>(request.CustomerBasicInfoDto);
                customer.PasswordHash = "";
                customer.Salt = "";
                customer = await _unitOfWork.CustomerRepository.Create(customer);
                await _unitOfWork.SaveAsync();
                return new BaseCommandResponse()
                {
                    Message = $"{_localizer[SharedResourcesKey.PhoneTOTPIs]} {_iTOTP.SendTOTP(_otpForPhone: true)} {_localizer[SharedResourcesKey.TOTPExpireAfter]}",
                    Payload = _mapper.Map<CustomerBasicInfoDto>(customer),
                    Code = StatusCodes.Status201Created,
                    Id = customer.Id
                };
            }
            else
            {
                _loggerManager.LogError($"{_localizer[SharedResourcesKey.CustomerCreationFailed]}");
                return new ErrorDetails
                {
                    Code = StatusCodes.Status400BadRequest,
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList(),
                    Message = $"{_localizer[SharedResourcesKey.CustomerCreationFailed]}",
                    RequestId = Guid.NewGuid(),
                };
            }
        }
    }
}
