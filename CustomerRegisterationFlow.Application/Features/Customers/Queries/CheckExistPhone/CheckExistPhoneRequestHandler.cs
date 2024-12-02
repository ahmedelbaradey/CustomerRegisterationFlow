using AutoMapper;
using AutoWrapper.Wrappers;
using CustomerRegisterationFlow.Application.Contracts.Infrastructure;
using CustomerRegisterationFlow.Application.Contracts.Presistence;
using CustomerRegisterationFlow.Application.DTOs.Customers;
using CustomerRegisterationFlow.Application.Responses;
using CustomerRegisterationFlow.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;


namespace CustomerRegisterationFlow.Application.Features.Customers.Queries.CheckExistPhoneRequest
{
    public class CheckExistPhoneRequestHandler : IRequestHandler<CheckExistPhoneRequest, IBaseResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _loggerManager;
        public CheckExistPhoneRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, ITOTP iTOTP, ILoggerManager loggerManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _loggerManager = loggerManager;
        }
        public async Task<IBaseResponse> Handle(CheckExistPhoneRequest request, CancellationToken cancellationToken)
        {
            if (request.Phone is null)
            {
                _loggerManager.LogError($"Phone is null");
                return new ErrorDetails
                {
                    Code = StatusCodes.Status404NotFound,
                    Message = $"Failed to fetch  database.",
                    RequestId = Guid.NewGuid()
                };
            }
            {
                return new BaseCommandResponse()
                {
                    Message = "Sucess",
                    Payload = await _unitOfWork.CustomerRepository.PhoneExists(request.Phone),
                    Code = StatusCodes.Status200OK,
                };
            }
        }
    }
}
