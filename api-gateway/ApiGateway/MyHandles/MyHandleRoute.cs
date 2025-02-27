using ApiGateway.Application.DTOs.Routes;
using ApiGateway.MyHelper;
using System.Net.Http;

namespace ApiGateway.MyHandles
{
    public static class MyHandleRoute
    {
        public static async Task HandleRouteAsync(HttpContext context, RouteConfigResponseDTO route, IHttpClientFactory httpClientFactory, ILogger logger)
        {
            try
            {
                var targetUrl = RouteHelper.BuildTargetUrl(context, route);
                logger.LogInformation($"Chuyển hướng yêu cầu từ {context.Request.Path} đến {targetUrl}");

                var httpClient = httpClientFactory.CreateClient();
                var requestMessage = RouteHelper.CreateRequestMessage(context, targetUrl);

                var response = await httpClient.SendAsync(requestMessage);
                await RouteHelper.CopyResponseToContextAsync(context, response);
            }
            catch (Exception ex)
            {
                //await RouteHelper.HandleError(context, logger, ex);
                throw new Exception("Lỗi xử lý yêu cầu thông qua API Gateway", ex);
            }
        }
    }
}
