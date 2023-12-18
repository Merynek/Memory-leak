using Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace Common.Dto
{
    public class CurrentUserDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public UserRole Role { get; set; }

        public CurrentUserDto(int id, string email, UserRole role)
        {
            Id = id;
            Email = email;
            Role = role;
        }
    }
}