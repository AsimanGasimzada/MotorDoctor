using MotorDoctor.Business;
using MotorDoctor.Business.Dtos;
using Newtonsoft.Json;
using System.Net;

namespace MotorDoctor.Presentation.Extensions;

public class GlobalExceptionHandler
{
    private readonly RequestDelegate _next;

    public GlobalExceptionHandler(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(context, e);
        }
    }
    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var statusCode = HttpStatusCode.InternalServerError;

        string errorName = "Xəta baş verdi";
        string errorMessage = "Gözlənilməyən xəta baş verdi. Server ilə əlaqə saxlayın";

        if (exception is KeyNotFoundException)
        {
            statusCode = HttpStatusCode.NotFound;
        }
        else if (exception is UnauthorizedAccessException)
        {
            statusCode = HttpStatusCode.Unauthorized;
            errorMessage = "Sizin bu hissəyə əlçatanlığınıza icazə verilmir";
        }
        else if (exception is IBaseException e)
        {
            statusCode = e.StatusCode;
            errorMessage = exception.Message;

        }

        context.Response.ContentType = "application/json";

        var errorDto = new ErrorDto
        {
            StatusCode = (int)statusCode,
            Message = errorMessage,
            Name = errorName
        };

        var result = JsonConvert.SerializeObject(errorDto);

        if (context.Request.Headers["Accept"].ToString().Contains("application/json"))
        {
            await context.Response.WriteAsync(result);
        }
        else
        {

            var errorPath = "/Home/Error";
            var query = $"?json={Uri.EscapeDataString(result)}"; 
            var fullPath = $"{errorPath}{query}";

            context.Response.Redirect(fullPath);
            await Task.CompletedTask;

        }
    }


}
