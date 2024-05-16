namespace ProductApi.Error
{
    public class ApiResponse
    {
        public int Status { get; set; }
        public string? Message { get; set; }
        public ApiResponse(int status,string? message=null)
        {
            Status = status;
            Message = message?? GetDefaultMessage(status);
        }

        private string? GetDefaultMessage(int s)
        {
            return s switch
            {
                400 => "Bad Request",
                401 => "UnAuthorized",
                404 => "Not Found",
                500 => "Server Error",
                _ => null
            };
        }

    }
}
