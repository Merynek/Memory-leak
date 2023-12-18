using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Dto
{
    public class UserActiveRequestDto
    {
        [Required]
        [Display(Name = "token")]
        public string Token { get; set; }
    }
}
