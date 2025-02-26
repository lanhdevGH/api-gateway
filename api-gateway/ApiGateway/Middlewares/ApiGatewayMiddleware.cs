
using ApiGateway.Application.Features.Routes;
using ApiGateway.MyHandles;

namespace ApiGateway.Middlewares
{
    public class ApiGatewayMiddleware : IMiddleware
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly GetRouteByPath _getRouteByPath;
        private readonly ILogger<ApiGatewayMiddleware> _logger;

        public ApiGatewayMiddleware(
            IHttpClientFactory httpClientFactory,
            GetRouteByPath getRouteByPath,
            ILogger<ApiGatewayMiddleware> logger)
        {
            _getRouteByPath = getRouteByPath;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var path = context.Request.Path.Value;
            if (string.IsNullOrEmpty(path))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Path không được để trống");
                return;
            }

            var route = await _getRouteByPath.ExecuteAsync(path);
            if (route == null)
            {
                // Không tìm thấy cấu hình định tuyến, chuyển tiếp cho middleware tiếp theo
                await next(context);
                return;
            }

            // Xử lý yêu cầu thông qua API Gateway
            await MyHandleRoute.HandleRouteAsync(context, route, _httpClientFactory, _logger);
        }
    }
}
