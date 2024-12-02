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
    public class GetCustomerDetailByIdRequestHandler : IRequestHandler<GetCustomerDetailByIdRequest, IBaseResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ITOTP _iTOTP;
        private readonly ILoggerManager _loggerManager;
        public GetCustomerDetailByIdRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, ITOTP iTOTP, ILoggerManager loggerManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _iTOTP = iTOTP;
            _loggerManager = loggerManager;
        }
        public async Task<IBaseResponse> Handle(GetCustomerDetailByIdRequest request, CancellationToken cancellationToken)
        {
            var customer = await _unitOfWork.CustomerRepository.FindByConditionAsync(c => c.Id.Equals(request.Id), trackChanges: false);
            if (customer is null)
            {
                _loggerManager.LogError($"The customer with ICNumber: {request.Id} doesn't exist in the database.");
               return new ErrorDetails
                {
                    Code = StatusCodes.Status404NotFound,
                    Message = $"The customer with ICNumber: {request.Id} doesn't exist in the database.",
                    RequestId = Guid.NewGuid(),
                };
            }
            {
                return new BaseCommandResponse()
                {
                    Message = $"Success",
                    Payload = _mapper.Map<CustomerBasicInfoDto>(customer),
                    Code = StatusCodes.Status200OK
                };
            }
        }
    }
}
