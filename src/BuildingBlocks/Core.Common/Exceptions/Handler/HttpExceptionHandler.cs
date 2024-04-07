using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Core.Common.Exceptions.Handler;

public class HttpExceptionHandler(ILogger<HttpExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogDebug("{exception} occurred at {time}",exception.Message,DateTime.Now);
        (string Details, string Title, int StatusCode) details = exception switch
        {
            NotFoundException => (exception.Message,exception.GetType().Name,StatusCodes.Status404NotFound),
            ValidationException => (exception.Message,exception.GetType().Name,StatusCodes.Status422UnprocessableEntity),
            InternalServerException => (exception.Message,exception.GetType().Name,StatusCodes.Status500InternalServerError),
            BadRequestException => (exception.Message,exception.GetType().Name,StatusCodes.Status400BadRequest),
            _ => (exception.Message, exception.GetType().Name, StatusCodes.Status500InternalServerError)
        };

        var problemDetails = new ProblemDetails
        {
            Title = details.Title,
            Status = details.StatusCode,
            Detail = details.Details,
            Instance = context.Request.Path
        };

        problemDetails.Extensions.Add("TraceId",context.TraceIdentifier);
        if (exception is ValidationException validationException)
            problemDetails.Extensions.Add("ValidationErrors", validationException.Errors);

        context.Response.StatusCode = details.StatusCode;
        await context.Response.WriteAsJsonAsync(problemDetails,cancellationToken);
        return true;
    }
}
