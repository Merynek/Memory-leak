using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Dto
{
    public class UserDetailResponseDto
    {
        [Required]
        public int Id { get; set; }
    }
}
