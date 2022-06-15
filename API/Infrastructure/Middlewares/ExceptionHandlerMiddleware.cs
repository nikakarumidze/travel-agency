using Newtonsoft.Json;

namespace API.Infrastructure.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var error = new ApiError(context, ex);
        var result = JsonConvert.SerializeObject(error);

        context.Response.Clear();
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = error.Status.Value;
        await context.Response.WriteAsync(result);
    }
}