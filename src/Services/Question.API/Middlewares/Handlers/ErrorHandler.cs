using Question.API.Exceptions;
using System.Net;
using System.Text.Json.Serialization;

namespace Question.API.Middlewares.Handlers
{
    public class ErrorHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandler> _logger;

        public ErrorHandler(RequestDelegate next, ILogger<ErrorHandler> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (System.Exception ex)
            {
                await HandleErrorAsync(httpContext, ex);
            }
        }

        private async Task HandleErrorAsync(HttpContext context, System.Exception exception)
        {
            var response = exception switch
            {
                NotFoundException => Error.buildNotFounError(exception),
                BadRequestException => Error.buildBadRequestError(exception),
                var otherError => Error.UnidentifiedError(exception)
            };

            _logger.LogError($"Error: {exception.Message} | Stack: {exception.StackTrace}");

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)response.StatusCode;
            await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
        }
    }
    class Error
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public static Error UnidentifiedError(System.Exception exception) => new Error()
        {
            Message = exception.Message,
            StatusCode = 500
        };

        public static Error buildNotFounError(System.Exception exception) => new Error()
        {
            Message = exception.Message,
            StatusCode = 404
        };

        public static Error buildBadRequestError(System.Exception exception) => new Error()
        {
            Message = exception.Message,
            StatusCode = 400
        };
    }
}
