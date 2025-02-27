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

        public static HttpRequestMessage CreateRequestMessage(HttpContext context, string targetUrl)
        {
            var requestMessage = new HttpRequestMessage
            {
                Method = new HttpMethod(context.Request.Method),
                RequestUri = new Uri(targetUrl)
            };

            CopyRequestHeaders(context, requestMessage);
            CopyRequestBody(context, requestMessage);

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

        public static void CopyRequestBody(HttpContext context, HttpRequestMessage requestMessage)
        {
            if (context.Request.ContentLength > 0)
            {
                var streamContent = new StreamContent(context.Request.Body);
                requestMessage.Content = streamContent;
            }
        }

        public static async Task<HttpResponseMessage> SendRequest(IHttpClientFactory httpClientFactory,
            HttpRequestMessage requestMessage)
        {
            var httpClient = httpClientFactory.CreateClient();
            return await httpClient.SendAsync(requestMessage);
        }

        public static async Task CopyResponseToContextAsync(HttpContext context, HttpResponseMessage response)
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

            // Không quên sao chép Content-* headers
            if (response.Content?.Headers != null)
            {
                foreach (var header in response.Content.Headers)
                {
                    context.Response.Headers[header.Key] = header.Value.ToArray();
                }
            }
        }

        public static async Task CopyResponseBody(HttpContext context, HttpResponseMessage response)
        {
            if (response.Content != null)
            {
                var responseStream = await response.Content.ReadAsStreamAsync();
                responseStream.CopyToAsync(context.Response.Body).Wait();
            }
        }
    }
}
