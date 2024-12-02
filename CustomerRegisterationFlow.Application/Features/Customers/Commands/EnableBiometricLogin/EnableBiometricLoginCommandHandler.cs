using AutoMapper;
using MediatR;
using CustomerRegisterationFlow.Application.Contracts.Presistence;
using CustomerRegisterationFlow.Application.Responses;
using CustomerRegisterationFlow.Application.DTOs.Customers.Validators;
using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Http;

namespace CustomerRegisterationFlow.Application.Features.Customers.Commands.EnableBiometricLogin
{
    public class EnableBiometricLoginCommandHandler : IRequestHandler<EnableBiometricLoginCommand, IBaseResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public EnableBiometricLoginCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IBaseResponse> Handle(EnableBiometricLoginCommand request, CancellationToken cancellationToken)
        {
            var validator = new EnableBiometricLoginDtoValidator();
            var validationResult = validator.Validate(request.EnableBiometricLoginDto);
            if (request.EnableBiometricLoginDto != null && validationResult.IsValid)
            {
                var customer = await _unitOfWork.CustomerRepository.FindAsync(request.EnableBiometricLoginDto.Id);
                customer.IsBiometricLoginEnabled = request.EnableBiometricLoginDto.IsBiometricLoginEnabled;
                await _unitOfWork.CustomerRepository.Update(customer);
                await _unitOfWork.SaveAsync();
                return new BaseCommandResponse()
                {
                    Message = $"BiometricLogin Enabled Sucessfully",
                    Code = StatusCodes.Status204NoContent,
                    Id = customer.Id
                };
            }
            else
            {
                return new ErrorDetails
                {
                    Code = StatusCodes.Status304NotModified,
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList(),
                    Message = $"BiometricLogin Enabled Sucessfully",
                    RequestId = Guid.NewGuid(),
                };
            }
        }
    }
}
