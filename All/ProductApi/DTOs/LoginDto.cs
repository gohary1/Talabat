using System.ComponentModel.DataAnnotations;

namespace ProductApi.DTOs
{
    public class LoginDto
    {
        [Required ( ErrorMessage = "email is required")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Pass is required")]
        public string Password { get; set; }
    }
}
