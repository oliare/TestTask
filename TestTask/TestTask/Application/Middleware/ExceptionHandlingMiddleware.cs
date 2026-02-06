using TestTask.Application.Exceptions;

namespace TestTask.Application.Middleware;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (EntityNotFoundException exception)
        {
            await SetResponse(context, StatusCodes.Status404NotFound, exception.Message);
            logger.LogWarning(exception, "Entity Not Found Exception occurred");
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Unhandled Exception occurred");
            await SetResponse(context, StatusCodes.Status500InternalServerError, exception.Message);
        }
    }

    private static async Task SetResponse(HttpContext context, int statusCode, string message, string? code = null)
    {
        object error = code == null
            ? new { Message = message }
            : new { Message = message, Code = code };

        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsJsonAsync(error);
    }
}

