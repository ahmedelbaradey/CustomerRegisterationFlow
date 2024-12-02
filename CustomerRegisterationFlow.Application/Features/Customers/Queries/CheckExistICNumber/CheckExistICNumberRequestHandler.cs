using AutoMapper;
using AutoWrapper.Wrappers;
using CustomerRegisterationFlow.Application.Contracts.Presistence;
using CustomerRegisterationFlow.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;


namespace CustomerRegisterationFlow.Application.Features.Customers.Queries.CheckExistICNumberRequest
{
    public class CheckExistICNumberRequestHandler : IRequestHandler<CheckExistICNumberRequest, IBaseResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CheckExistICNumberRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IBaseResponse> Handle(CheckExistICNumberRequest request, CancellationToken cancellationToken)
        {
            if (!request.ICNumber.HasValue)
               return new ErrorDetails
                {
                    Code = StatusCodes.Status404NotFound,
                    Message = $"Failed to fetch  database.",
                    RequestId = Guid.NewGuid(),
                };
            return new BaseCommandResponse()
            {
                Message = "Sucess",
                Payload = await _unitOfWork.CustomerRepository.ICNumberExists(request.ICNumber.Value),
                Code = StatusCodes.Status200OK,
            };
        }
    }
}
