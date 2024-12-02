using AutoMapper;
using AutoWrapper.Wrappers;
using CustomerRegisterationFlow.Application.Contracts.Presistence;
using CustomerRegisterationFlow.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;


namespace CustomerRegisterationFlow.Application.Features.Customers.Queries.CheckExistEmail
{
    public class CheckExistEmailRequestHandler : IRequestHandler<CheckExistEmailRequest, IBaseResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CheckExistEmailRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IBaseResponse> Handle(CheckExistEmailRequest request, CancellationToken cancellationToken)
        {
            if (request.Email is null)
               return new ErrorDetails
                {
                    Code = StatusCodes.Status404NotFound,
                    Message = $"Failed to fetch  database.",
                    RequestId = Guid.NewGuid(),
                };
            return new BaseCommandResponse()
            {
                Message = "Sucess",
                Payload = await _unitOfWork.CustomerRepository.EmailExists(request.Email),
                Code = StatusCodes.Status200OK
            };
        }
    }
}
