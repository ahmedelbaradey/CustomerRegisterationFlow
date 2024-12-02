using AutoWrapper.Wrappers;
using CustomerRegisterationFlow.Application.Exceptions;
using CustomerRegisterationFlow.Application.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
namespace CustomerRegisterationFlow.Presentation.ActionFilters
{
    public class ValidationFilterAttribute : IActionFilter
    {

         public ValidationFilterAttribute()
        { }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var action = context.RouteData.Values["action"];
            var controller = context.RouteData.Values["controller"];
            var param = context.ActionArguments.SingleOrDefault(x => x.Value.ToString().Contains("Dto")).Value;
            if (param is null)
            {
                throw new ApiException(new ErrorDetails
                {
                    Code = StatusCodes.Status422UnprocessableEntity,
                    Message = $"Empty Parsed Data",
                    RequestId = Guid.NewGuid(),
                    Status = false
                }, StatusCodes.Status422UnprocessableEntity);
            }
        }
        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
