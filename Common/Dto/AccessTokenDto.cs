using System;
using System.ComponentModel.DataAnnotations;

namespace Common.Dto
{
    public class AccessTokenDto
    {
        [Required]
        public string Token { get; set; }
        [Required]
        public DateTimeOffset ExpireDate { get; set; }
    }
}