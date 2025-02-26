
using ApiGateway.MyHandles;

namespace ApiGateway.Middlewares
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware( ILogger<ExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                // Gọi middleware tiếp theo trong pipeline
                await next(context);
            }
            catch (Exception ex)
            {
                // Xử lý exception
                await MyHandleException.HandleExceptionAsync(context, ex, _logger);
            }
        }
    }
}
