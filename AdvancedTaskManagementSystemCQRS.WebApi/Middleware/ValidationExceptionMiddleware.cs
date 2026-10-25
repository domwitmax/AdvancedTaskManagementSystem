using FluentValidation;

namespace AdvancedTaskManagementSystemCQRS.WebApi.Middleware;

public class ValidationExceptionMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException exception)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(exception.Errors, cancellationToken: context.RequestAborted);
        }
    }
}
