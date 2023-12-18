using System.ComponentModel.DataAnnotations;

namespace Common.Dto
{
    public class LoginResponseDto
    {
        [Required]
        public CurrentUserDto user { get; set; }
        [Required]
        public AccessTokenDto Token { get; set; }

        [Required]
        public AppBusinessConfigResponseDto appBusinessConfig { get; set; }
    }
}