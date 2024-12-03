using AutoMapper;
using MediatR;
using CustomerRegisterationFlow.Application.Contracts.Presistence;
using CustomerRegisterationFlow.Application.Responses;
using CustomerRegisterationFlow.Application.DTOs.Customers.Validators;
using CustomerRegisterationFlow.Application.Features.LeaveRequests.Commands.HasAcceptTerms;
using FluentValidation;
using AutoWrapper.Wrappers;
using CustomerRegisterationFlow.Application.Contracts.Infrastructure;
using Microsoft.AspNetCore.Http;
using CustomerRegisterationFlow.Domain.Entities;
using CustomerRegisterationFlow.Application.DTOs.Customers;
using Microsoft.Extensions.Localization;
using CustomerRegisterationFlow.Resources;

namespace CustomerRegisterationFlow.Application.Features.Customers.Commands.VerifyCustomerEmail
{
    public class HasAcceptTermsCommandHandler : IRequestHandler<HasAcceptTermsCommand, IBaseResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        public HasAcceptTermsCommandHandler(IUnitOfWork unitOfWork, IMapper mapper , IStringLocalizer<SharedResources> localizer ) 
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }
        public async Task<IBaseResponse> Handle(HasAcceptTermsCommand request, CancellationToken cancellationToken)
        {
            var validator = new HasAcceptedTermsDtoValidator(_localizer);
            var validationResult = validator.Validate(request.HasAcceptedTermsDto);
            if (request.HasAcceptedTermsDto != null && validationResult.IsValid)
            {
                return new BaseCommandResponse()
                {
                    Message = $"{_localizer[SharedResourcesKey.TermsAcceptedSucessfully]}",
                    Code = StatusCodes.Status204NoContent,
                    Id = request.HasAcceptedTermsDto.Id
                };
            }
            else
            {
                return new ErrorDetails
                {
                    Code = StatusCodes.Status304NotModified,
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList(),
                    Message = $"{_localizer[SharedResourcesKey.TermsAcceptedFailed]}",
                    RequestId = Guid.NewGuid(),
                };
            }
        }
    }
}
