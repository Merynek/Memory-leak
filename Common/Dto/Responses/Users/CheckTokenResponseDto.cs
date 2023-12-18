using Common.AppSettings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Dto
{
    public class CheckTokenResponseDto
    {
        [Required]
        public CurrentUserDto user { get; set; }

        [Required]
        public AppBusinessConfigResponseDto appBusinessConfig { get; set; }
    }
}
