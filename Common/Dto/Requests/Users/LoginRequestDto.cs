using System.ComponentModel.DataAnnotations;

namespace Common.Dto
{
    public class LoginRequestDto
    {
        [Required]
        [Display(Name = "email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "password")]
        public string Password { get; set; }

    }
}
