using LawyerAssistant.Application.Objects;

namespace LawyerAssistant.PanelAPI;

public class CustomExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<CustomExceptionMiddleware> _logger;
    private readonly string _defaultErrorMessage;

    public CustomExceptionMiddleware(RequestDelegate next, ILogger<CustomExceptionMiddleware> logger, IConfiguration configuration)
    {
        _next = next;
        _logger = logger;
        _defaultErrorMessage = configuration["DefaultErrorMessage"] ?? "خطایی رخ داده است.";
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context); 
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        AddLogs(exception.Message, context.Request.Path);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status400BadRequest;

        var isCustomException = exception.GetType().Name == "CustomException";

        var response = new SysResult
        {
            IsSuccess = false,
            Message = isCustomException ? exception.Message : exception.Message
        };

        var result = System.Text.Json.JsonSerializer.Serialize(response);
        await context.Response.WriteAsync(result);
    }

    private void AddLogs(string message, string path)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        _logger.LogError("Exception occurred at path: {Path} | Message: {Message}", path, message);
    }
}
