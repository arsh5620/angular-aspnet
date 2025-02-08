using System.Net;
using System.Text.Json;

namespace API;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment host)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new ApiError(context.Response.StatusCode, ex.Message);
            var jsonSerializeOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            var json = JsonSerializer.Serialize(response, jsonSerializeOptions);
            await context.Response.WriteAsync(json);
        }
    }

    class ApiError(int statusCode, string errorMessage)
    {
        public int StatusCode { get; set; } = statusCode;
        public string ErrorMessage { get; set; } = errorMessage;
    }
}
