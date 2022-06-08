using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace API.Infrastructure.Middlewares;

public sealed class ApiError : ProblemDetails
{
    private const string UnhandledError = "UnhandledException";
    private LogLevel LogLevel { get; set; }
    private string Code { get; set; }
        
    private string TraceId
    {
        get
        {
            if (Extensions.TryGetValue("TraceId", out var traceId))
            {
                return (string)traceId;
            }

            return null;
        }
        set => Extensions["TraceId"] = value;
    }

    public ApiError(HttpContext context, Exception exception)
    {
        TraceId = context.TraceIdentifier;
        Code = UnhandledError;
        Title = exception.Message;
        LogLevel = LogLevel.None;
        Instance = context.Request.Path;
        HandleException((dynamic)exception);
    }

    private void HandleException(Exception exception)
    {
        Code = UnhandledError;
        Status = (int)HttpStatusCode.InternalServerError;
        Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1";
        Title = exception.Message;
        LogLevel = LogLevel.Error;
    }
}