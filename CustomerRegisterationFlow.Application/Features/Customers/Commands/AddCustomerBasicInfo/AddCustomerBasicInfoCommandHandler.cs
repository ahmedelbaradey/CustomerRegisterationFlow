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

namespace CustomerRegisterationFlow.Application.Features.Customers.Commands.AddCustomerBasicInfo
{
    public class AddCustomerBasicInfoCommandHandler : IRequestHandler<AddCustomerBasicInfoCommand, IBaseResponse>
    {
       
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ITOTP _iTOTP;
        private readonly ILoggerManager _loggerManager;
        public AddCustomerBasicInfoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ITOTP iTOTP, ILoggerManager loggerManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _iTOTP = iTOTP;
            _loggerManager = loggerManager;
        }

        public async Task<IBaseResponse> Handle(AddCustomerBasicInfoCommand request, CancellationToken cancellationToken)
        {
            var validator = new CustomerBasicInfoDtoValidator(_unitOfWork.CustomerRepository);
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
                    Message = $"Phone TOTP is : {_iTOTP.SendTOTP(_otpForPhone: true)} and will expire after 2 minutes",
                    Payload = _mapper.Map<CustomerBasicInfoDto>(customer),
                    Code = StatusCodes.Status201Created,
                    Id = customer.Id
                };
            }
            else
            {
                _loggerManager.LogError($"Customer Creation Failed");
                return new ErrorDetails
                {
                    Code = StatusCodes.Status400BadRequest,
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList(),
                    Message = $"Customer Creation Failed",
                    RequestId = Guid.NewGuid(),
                };
            }
        }
    }
}
