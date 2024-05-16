namespace ProductApi.Error
{
    public class ApiValidationErrorRes:ApiResponse
    {
        public IEnumerable<string> Errors { get; set; }
        public ApiValidationErrorRes():base(400)
        {
            Errors = new List<string>(); 
        }
    }
}
