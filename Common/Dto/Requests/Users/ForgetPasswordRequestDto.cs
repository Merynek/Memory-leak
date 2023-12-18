using System.ComponentModel.DataAnnotations;

namespace Common.Dto
{
    public class ForgetPasswordRequestDto
    {
        [Required]
        [Display(Name = "email")]
        [EmailAddress]
        public string Email { get; set; }

    }
}
