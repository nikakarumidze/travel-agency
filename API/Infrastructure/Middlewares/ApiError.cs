using System.Net;
using Microsoft.AspNetCore.Mvc;
using Services.Exceptions;

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
    private void HandleException(NotFoundException exception)
    {
        Code = exception.Code;
        Status = (int)HttpStatusCode.NotFound;
        Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4";
        Title = exception.Message;
        LogLevel = LogLevel.Error;
    }
    private void HandleException(InvalidCredentialsException exception)
    {
        Code = exception.Code;
        Status = (int) HttpStatusCode.Unauthorized;
        Title = exception.Message;
        LogLevel = LogLevel.Error;
    }
    private void HandleException(ObjectAlreadyExistsException exception)
    {
        Code = exception.Code;
        Status = (int) HttpStatusCode.Conflict;
        Title = exception.Message;
        LogLevel = LogLevel.Error;
    }
    
    private void HandleException(ApartmentNotAvailableException exception)
    {
        Code = exception.Code;
        Status = (int) HttpStatusCode.Conflict;
        Title = exception.Message;
        LogLevel = LogLevel.Error;
    }
}