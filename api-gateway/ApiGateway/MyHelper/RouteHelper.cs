using ApiGateway.Application.DTOs.Routes;

namespace ApiGateway.MyHelper
{
    public static class RouteHelper
    {
        public static string BuildTargetUrl(HttpContext context, RouteConfigResponseDTO route)
        {
            var targetPath = context.Request.Path.Value?.Replace(route.Path, route.TargetPath);
            return $"{route.TargetService}{targetPath}{context.Request.QueryString}";
        }

        public static async Task<HttpRequestMessage> CreateRequestMessage(HttpContext context, string targetUrl)
        {
            var requestMessage = new HttpRequestMessage
            {
                Method = new HttpMethod(context.Request.Method),
                RequestUri = new Uri(targetUrl)
            };

            CopyRequestHeaders(context, requestMessage);
            await CopyRequestBody(context, requestMessage);

            return requestMessage;
        }

        public static void CopyRequestHeaders(HttpContext context, HttpRequestMessage requestMessage)
        {
            foreach (var header in context.Request.Headers)
            {
                if (!header.Key.StartsWith("Host", StringComparison.OrdinalIgnoreCase))
                {
                    requestMessage.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
                }
            }
        }

        public static async Task CopyRequestBody(HttpContext context, HttpRequestMessage requestMessage)
        {
            if (context.Request.ContentLength > 0)
            {
                var streamContent = new StreamContent(context.Request.Body);
                requestMessage.Content = streamContent;
                await Task.CompletedTask; // Added await to avoid CS1998 warning
            }
        }

        public static async Task<HttpResponseMessage> SendRequest(IHttpClientFactory httpClientFactory,
            HttpRequestMessage requestMessage)
        {
            var httpClient = httpClientFactory.CreateClient();
            return await httpClient.SendAsync(requestMessage);
        }

        public static async Task ProcessResponse(HttpContext context, HttpResponseMessage response)
        {
            CopyResponseHeaders(context, response);
            context.Response.StatusCode = (int)response.StatusCode;
            await CopyResponseBody(context, response);
        }

        public static void CopyResponseHeaders(HttpContext context, HttpResponseMessage response)
        {
            foreach (var header in response.Headers)
            {
                context.Response.Headers[header.Key] = header.Value.ToArray();
            }
        }

        public static async Task CopyResponseBody(HttpContext context, HttpResponseMessage response)
        {
            if (response.Content != null)
            {
                var responseStream = await response.Content.ReadAsStreamAsync();
                await responseStream.CopyToAsync(context.Response.Body);
            }
        }

        public static async Task HandleError(HttpContext context, ILogger logger, Exception ex)
        {
            logger.LogError(ex, "Lỗi khi xử lý yêu cầu thông qua API Gateway");
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            var error = new { message = "Lỗi khi xử lý yêu cầu API Gateway", error = ex.Message };
            await context.Response.WriteAsJsonAsync(error);
        }
    }
}
