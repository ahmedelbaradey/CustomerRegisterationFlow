using API.Services.Base;
using CustomerRegisterationFlow.Application.Contracts.Infrastructure;
using CustomerRegisterationFlow.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;
using System;


namespace API.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {

        public static void ConfigureExceptionHandler(this WebApplication app, ILoggerManager logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        var response = new Response<ErrorDetails>
                        {
                            Message = contextFeature.Error.Message,
                            Success = false
                        };
                        switch (contextFeature.Error)
                        {
                            case BadRequestException badRequestException:
                                response.StatusCode = StatusCodes.Status400BadRequest;
                                break;
                            case ValidationException validationException:
                                response.StatusCode = StatusCodes.Status400BadRequest;
                                response.ValidationErrors = validationException.Errors;
                                break;
                            case NotFoundException notFoundException:
                                response.StatusCode = StatusCodes.Status404NotFound;
                                break;
                            default:
                                response.StatusCode = StatusCodes.Status500InternalServerError;
                                break;
                        }

                        string result = JsonConvert.SerializeObject(response);
                        logger.LogError($"Something went wrong: {contextFeature.Error}");

                        await context.Response.WriteAsync(result);
                    }
                });
            });
        }
    }
}
