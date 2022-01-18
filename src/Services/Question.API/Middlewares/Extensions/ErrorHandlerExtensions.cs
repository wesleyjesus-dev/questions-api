using Question.API.Middlewares.Handlers;

namespace Question.API.Middlewares.Extensions
{
    public static class ErrorHandlerExtensions
    {
        public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder appBuilder)
        {
            return appBuilder.UseMiddleware<ErrorHandler>();
        }
    }
}
