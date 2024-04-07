using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Core.Common.Behaviours;

public class LoggingBehaviour<TRequest, TResponse>(ILogger<LoggingBehaviour<TRequest,TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
    where TResponse : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        logger.LogInformation("[Start] Handle Request - {request}, Response - {response}, Request Data - {data}", typeof(TRequest).Name, typeof(TResponse).Name, request);
        var timer = new Stopwatch();
        timer.Start();
        var response = await next();
        timer.Stop();
        var timeTaken = timer.Elapsed;
        if(timeTaken.Seconds > 3)
            logger.LogWarning("[Performance] The {request} took {time} sec.",typeof(TRequest).Name,timeTaken.Seconds);
        logger.LogInformation("[End] Handle Request - {request}, Response - {response}, Response Data - {data}", typeof(TRequest).Name, typeof(TResponse).Name, response);
        return response;
    }
}
