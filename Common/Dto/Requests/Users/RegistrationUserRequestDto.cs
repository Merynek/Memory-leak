using Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace Common.Dto
{
    public class RegistrationUserRequestDto
    {
        [Required]
        [Display(Name = "email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "confirmPassword")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "role")]
        public UserRole Role { get; set; }

    }

}