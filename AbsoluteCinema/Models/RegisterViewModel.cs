using System.ComponentModel.DataAnnotations;

namespace AbsoluteCinema.Models
{
    public class RegisterViewModel
    {
        [Required]
        [MinLength(6)]
        public string Username { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [RegularExpression(@".*\.com$", ErrorMessage = "Email must end with .com.")]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
