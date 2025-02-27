namespace ApiGateway.MyHandles
{
    public static class MyHandleException
    {
        public static async Task HandleExceptionAsync(HttpContext context, Exception exception, ILogger logger)
        {
            // Ghi log lỗi
            logger.LogError(exception, "Lỗi xử lý yêu cầu: {Message}", exception.Message);

            // Đặt lại response
            context.Response.Clear();
            context.Response.ContentType = "application/json";

            // Xác định mã trạng thái HTTP dựa trên loại exception
            var statusCode = DetermineStatusCode(exception);
            context.Response.StatusCode = statusCode;

            // Tạo đối tượng lỗi để trả về
            var errorResponse = CreateErrorResponse(exception, statusCode);

            // Ghi response
            await context.Response.WriteAsJsonAsync(errorResponse);
        }

        public static int DetermineStatusCode(Exception exception)
        {
            // Có thể mở rộng logic này để xử lý các loại exception khác nhau
            return exception switch
            {
                ArgumentException => StatusCodes.Status400BadRequest,
                UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                KeyNotFoundException => StatusCodes.Status404NotFound,
                TimeoutException => StatusCodes.Status408RequestTimeout,
                // Có thể thêm các loại exception khác
                _ => StatusCodes.Status500InternalServerError
            };
        }

        public static object CreateErrorResponse(Exception exception, int statusCode)
        {
            // Tạo đối tượng lỗi chuẩn hóa
            var isProduction = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production";

            // Trong môi trường production, không nên hiển thị chi tiết exception
            if (isProduction)
            {
                return new
                {
                    status = statusCode,
                    message = "Đã xảy ra lỗi khi xử lý yêu cầu của bạn.",
                    traceId = Guid.NewGuid().ToString()
                };
            }

            // Trong môi trường phát triển, hiển thị thêm chi tiết để gỡ lỗi
            return new
            {
                status = statusCode,
                message = "Đã xảy ra lỗi khi xử lý yêu cầu của bạn.",
                detail = exception.Message,
                //stackTrace = exception.StackTrace,
                //innerException = exception.InnerException?.Message,
                //exceptionType = exception.GetType().Name,
                //traceId = Guid.NewGuid().ToString()
            };
        }
    }
}
