using System.ComponentModel.DataAnnotations;

namespace ProductApi.DTOs
{
    public class RegisterDto
    {
        [Required(ErrorMessage ="Name is req")]
        public string DisplayName { get; set; }
        [Required(ErrorMessage = "email is req")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Pass is req")]
        public string Password { get; set; }
        [Required(ErrorMessage = "phonnumber is req")]
        public string PhoneNumber { get; set; }

    }
}
