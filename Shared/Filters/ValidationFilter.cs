using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Shared.Filters;

public class ValidationFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        Console.WriteLine($"csdddddddddddddddddddddddddddddddddddddddd/n/n/n/n\n\n\n\n\n{context.ModelState.IsValid}");
        if (!context.ModelState.IsValid)
        {
            var errorMessage = context.ModelState.Values.SelectMany(x => x.Errors).Last().ErrorMessage;

            context.Result = new BadRequestObjectResult(new ErrorResponse { ErrorCode = 1, Message = errorMessage });
        }
        else
        {
            await next();
        }
    }
}
