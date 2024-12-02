using AutoMapper;
using AutoWrapper.Wrappers;
using CustomerRegisterationFlow.Application.Contracts.Infrastructure;
using CustomerRegisterationFlow.Application.Contracts.Presistence;
using CustomerRegisterationFlow.Application.DTOs.Customers;
using CustomerRegisterationFlow.Application.Exceptions;
using CustomerRegisterationFlow.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CustomerRegisterationFlow.Application.Features.Customers.Queries.Get
{
    public class GetCustomerDetailRequestHandler : IRequestHandler<GetCustomerDetailRequest, IBaseResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ITOTP _iTOTP;
        private readonly ILoggerManager _loggerManager;
        public GetCustomerDetailRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, ITOTP iTOTP, ILoggerManager loggerManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _iTOTP = iTOTP;
            _loggerManager = loggerManager;
        }
        public async Task<IBaseResponse> Handle(GetCustomerDetailRequest request, CancellationToken cancellationToken)
        {
            var customer = await _unitOfWork.CustomerRepository.FindByConditionAsync(c => c.ICNumber.Equals(request.BaseCustomerDto.ICNumber), trackChanges: false);
            if (customer is null)
            {
                _loggerManager.LogError($"The customer with ICNumber: {request.BaseCustomerDto.ICNumber} doesn't exist in the database.");
               return new ErrorDetails
                {
                    Code = StatusCodes.Status404NotFound,
                    Message = $"The customer with ICNumber: {request.BaseCustomerDto.ICNumber} doesn't exist in the database.",
                    RequestId = Guid.NewGuid(),
                };
            }
            {
                return new BaseCommandResponse()
                {
                    Message = $"Phone TOTP is : {_iTOTP.SendTOTP(_otpForPhone: true)} and will expire after 2 minutes",
                    Payload = _mapper.Map<CustomerBasicInfoDto>(customer),
                    Code = StatusCodes.Status200OK
                };
            }
        }
    }
}
