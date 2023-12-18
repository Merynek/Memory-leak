using System;

namespace Common.Dto
{
    public class RefreshTokenDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
        public DateTimeOffset ExpireDate { get; set; }
        public virtual UserDto User { get; set; }
    }
}