using System.Collections.Generic;
using Common.Enums;

namespace Common.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
        public bool Banned { get; set; } = false;
        public bool Active { get; set; } = false;
        public virtual ICollection<RefreshTokenDto> RefreshTokens { get; set; }

        public UserDto(RegistrationUserRequestDto req)
        {
            Email = req.Email;
            Password = req.Password;
            Role = req.Role;
        }

        public UserDto()
        { }
    }
    
}