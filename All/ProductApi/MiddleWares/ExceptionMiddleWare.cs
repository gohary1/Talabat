using ProductApi.Error;
using System.Net;
using System.Text.Json;

namespace ProductApi.MiddleWares
{
    public class ExceptionMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleWare> _logger;
        private readonly IWebHostEnvironment _env;

        public ExceptionMiddleWare(RequestDelegate next,ILogger<ExceptionMiddleWare> logger,IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                var response = _env.IsDevelopment() ? new ApiExceptionRes(httpContext.Response.StatusCode, ex.Message, ex.StackTrace.ToString())
                    : new ApiExceptionRes(httpContext.Response.StatusCode);
                var json=JsonSerializer.Serialize(response);
                await httpContext.Response.WriteAsync(json);
            }
        }

    }
}
