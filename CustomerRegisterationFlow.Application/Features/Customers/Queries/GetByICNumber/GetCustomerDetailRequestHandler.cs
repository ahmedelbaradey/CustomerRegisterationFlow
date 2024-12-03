using AutoMapper;
using AutoWrapper.Wrappers;
using CustomerRegisterationFlow.Application.Contracts.Infrastructure;
using CustomerRegisterationFlow.Application.Contracts.Presistence;
using CustomerRegisterationFlow.Application.DTOs.Customers;
using CustomerRegisterationFlow.Application.Exceptions;
using CustomerRegisterationFlow.Application.Responses;
using CustomerRegisterationFlow.Resources;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;

namespace CustomerRegisterationFlow.Application.Features.Customers.Queries.Get
{
    public class GetCustomerDetailRequestHandler : IRequestHandler<GetCustomerDetailRequest, IBaseResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ITOTP _iTOTP;
        private readonly ILoggerManager _loggerManager;
        private readonly IStringLocalizer<SharedResources> _localizer;
        public GetCustomerDetailRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, ITOTP iTOTP, ILoggerManager loggerManager, IStringLocalizer<SharedResources> localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _iTOTP = iTOTP;
            _loggerManager = loggerManager;
            _localizer = localizer;
        }
        public async Task<IBaseResponse> Handle(GetCustomerDetailRequest request, CancellationToken cancellationToken)
        {
            var customer = await _unitOfWork.CustomerRepository.FindByConditionAsync(c => c.ICNumber.Equals(request.BaseCustomerDto.ICNumber), trackChanges: false);
            if (customer is null)
            {
                _loggerManager.LogError($"{_localizer[SharedResourcesKey.TheCustomerWithICNumber]} {request.BaseCustomerDto.ICNumber} {_localizer[SharedResourcesKey.DoesntExist]}");
               return new ErrorDetails
                {
                    Code = StatusCodes.Status404NotFound,
                    Message = $"{_localizer[SharedResourcesKey.TheCustomerWithICNumber]} {request.BaseCustomerDto.ICNumber} {_localizer[SharedResourcesKey.DoesntExist]}",
                    RequestId = Guid.NewGuid(),
                };
            }
            {
                return new BaseCommandResponse()
                {
                    Message = $"{_localizer[SharedResourcesKey.PhoneTOTPIs]} {_iTOTP.SendTOTP(_otpForPhone: true)} {_localizer[SharedResourcesKey.TOTPExpireAfter]}",
                    Payload = _mapper.Map<CustomerBasicInfoDto>(customer),
                    Code = StatusCodes.Status200OK
                };
            }
        }
    }
}
