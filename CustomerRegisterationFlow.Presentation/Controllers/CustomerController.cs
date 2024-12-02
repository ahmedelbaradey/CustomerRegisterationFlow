using CustomerRegisterationFlow.Application.Features.Customers.Queries.Get;
using CustomerRegisterationFlow.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CustomerRegisterationFlow.Application.DTOs.Customers;
using Asp.Versioning;
using CustomerRegisterationFlow.Presentation.ActionFilters;
using CustomerRegisterationFlow.Application.Features.Customers.Commands.AddCustomerBasicInfo;
using Microsoft.AspNetCore.Http;
using CustomerRegisterationFlow.Application.Features.Customers.Commands.VerifyCustomerPhone;
using CustomerRegisterationFlow.Application.Features.Customers.Commands.VerifyCustomerEmail;
using CustomerRegisterationFlow.Application.Features.LeaveRequests.Commands.HasAcceptTerms;
using CustomerRegisterationFlow.Application.Features.Customers.Commands.CreatePIN;
using CustomerRegisterationFlow.Application.Features.Customers.Commands.EnableBiometricLogin;
using CustomerRegisterationFlow.Application.Contracts.Infrastructure;
using CustomerRegisterationFlow.Application.Features.Customers.Commands.VerifyPIN;
using System.Net;
using Microsoft.AspNetCore.Http.HttpResults;
using AutoWrapper.Wrappers;
namespace CustomerRegisterationFlow.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    [ApiExplorerSettings(GroupName = "v2")]
    public class CustomerController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly ILoggerManager _loggerManager;
        public CustomerController(IMediator mediator,ILoggerManager loggerManager)
        {
            _mediator = mediator;
            _loggerManager = loggerManager;
        }

        [HttpGet("{id}", Name = "CustomerById")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var command = new GetCustomerDetailByIdRequest { Id = id };
                var result = await _mediator.Send(command);
                return CreatedAtRoute("CustomerById", new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex.Message);
                throw new ApiException(new ErrorDetails() { Message = ex.Message }, StatusCodes.Status400BadRequest);
            }
        }


        [HttpPost("Register")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        public async Task<IActionResult> AddCustomerBasicInfo([FromBody] CustomerBasicInfoDto customerBasicInfoDto)
        {
            try
            {
                var command = new AddCustomerBasicInfoCommand { CustomerBasicInfoDto = customerBasicInfoDto };
                var result = await _mediator.Send(command);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex.Message);
                throw new ApiException(new ErrorDetails() { Message = ex.Message },StatusCodes.Status400BadRequest);
            }
        }

        [HttpPost("Login")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(422)]
        public async Task<ActionResult<BaseCommandResponse>> Login(BaseCustomerDto baseCustomerDto)
        {
            try
            {
                var command = new GetCustomerDetailRequest { BaseCustomerDto = baseCustomerDto };
                var result = await _mediator.Send(command);
                return Ok(result);
                //return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex.Message);
                throw new ApiException(new ErrorDetails() { Message = ex.Message }, StatusCodes.Status400BadRequest);
            }
        }
        [HttpPost("VerifyPhone")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ProducesResponseType(204)]
        [ProducesResponseType(304)]
        [ProducesResponseType(422)]
        public async Task<IActionResult> VerifyPhone([FromBody] VerifyPhoneDto verifyPhoneDto)
        {
            try
            {
                var command = new VerifyCustomerPhoneCommand { VerifyPhoneDto = verifyPhoneDto };
                var result = await _mediator.Send(command);
                return Ok(result);
                //return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex.Message);
                throw new ApiException(new ErrorDetails() { Message = ex.Message }, StatusCodes.Status400BadRequest);
            }
        }

        [HttpPost("VerifyEmail")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ProducesResponseType(204)]
        [ProducesResponseType(304)]
        [ProducesResponseType(422)]
        public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmailDto verifyEmailDto)
        {
            try
            {
                var command = new VerifyCustomerEmailCommand { VerifyEmailDto = verifyEmailDto };
                var result = await _mediator.Send(command);
                return Ok(result);
                //return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex.Message);
                throw new ApiException(new ErrorDetails() { Message = ex.Message }, StatusCodes.Status400BadRequest);
            }
        }

        [HttpPost("AcceptTerms")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ProducesResponseType(204)]
        [ProducesResponseType(304)]
        [ProducesResponseType(422)]
        public async Task<IActionResult> AcceptTerms([FromBody] HasAcceptedTermsDto hasAcceptedTermsDto)
        {
            try
            {
                var command = new HasAcceptTermsCommand { HasAcceptedTermsDto = hasAcceptedTermsDto };
                var result = await _mediator.Send(command);
                return Ok(result);
                //return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex.Message);
                throw new ApiException(new ErrorDetails() { Message = ex.Message }, StatusCodes.Status400BadRequest);
            }
        }

        [HttpPost("CreatePIN")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ProducesResponseType(204)]
        [ProducesResponseType(304)]
        [ProducesResponseType(422)]
        public async Task<IActionResult> CreatePINCode([FromBody] CreatePINDto createPINDto)
        {
            try
            {
                var command = new CreatePINCommand { CreatePINDto = createPINDto };
                var result = await _mediator.Send(command);
                return Ok(result);
                //return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex.Message);
                throw new ApiException(new ErrorDetails() { Message = ex.Message }, StatusCodes.Status400BadRequest);
            }
        }

        [HttpPost("VerifyPIN")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ProducesResponseType(204)]
        [ProducesResponseType(304)]
        [ProducesResponseType(422)]
        public async Task<IActionResult> VerifyPIN([FromBody] VerifyPINDto verifyPINDto)
        {
            try
            {
                var command = new VerifyPINCommand { VerifyPINDto = verifyPINDto };
                var result = await _mediator.Send(command);
                return Ok(result);
                //return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex.Message);
                throw new ApiException(new ErrorDetails() { Message = ex.Message }, StatusCodes.Status400BadRequest);
            }
        }

        [HttpPost("EnableBiometricLogin")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ProducesResponseType(204)]
        [ProducesResponseType(304)]
        [ProducesResponseType(422)]
        public async Task<IActionResult> EnableBiometricLogin([FromBody] EnableBiometricLoginDto enableBiometricLoginDto)
        {
            try
            {
                var command = new EnableBiometricLoginCommand { EnableBiometricLoginDto = enableBiometricLoginDto };
                var result = await _mediator.Send(command);
                return Ok(result);
                //return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex.Message);
                throw new ApiException(new ErrorDetails() { Message = ex.Message }, StatusCodes.Status400BadRequest);
            }
        }
    }
}
