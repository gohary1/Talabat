namespace ProductApi.Error
{
    public class ApiExceptionRes:ApiResponse
    {
        public string? Details { get; set; }
        public ApiExceptionRes(int statusCode,string? message,string? details):base(statusCode, message)
        {
            Details = details;
        }
        public ApiExceptionRes(int statusCode):base(statusCode)
        {
            
        }
    }
}
