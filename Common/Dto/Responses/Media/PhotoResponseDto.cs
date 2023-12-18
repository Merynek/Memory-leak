using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Dto
{
    public class PhotoResponseDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Path { get; set; }
    }
}
