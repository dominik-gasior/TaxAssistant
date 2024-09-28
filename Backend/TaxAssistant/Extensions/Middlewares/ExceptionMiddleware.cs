using System.Net;
using TaxAssistant.Extensions.Exceptions;

namespace TaxAssistant.Extensions.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (NotFoundException ex)
        {
            _logger.LogError(ex.Message);
            await HandleExceptionAsync(httpContext, ex, HttpStatusCode.NotFound);
        }
        catch (BadRequestException ex)
        {
            _logger.LogError(ex.Message);
            await HandleExceptionAsync(httpContext, ex, HttpStatusCode.BadRequest);
        }
        catch (Exception ex)
        {
            _logger.LogCritical($"Something went wrong: {ex.Message}");
            await HandleExceptionAsync(httpContext, ex, HttpStatusCode.InternalServerError);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception, HttpStatusCode statusCode)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var result = new
        {
            StatusCode = context.Response.StatusCode,
            Details = exception.Message
        };

        return context.Response.WriteAsJsonAsync(result);
    }
}