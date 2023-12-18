using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Dto
{
    public class GetUsersDetailRequestDto
    {
        [Required]
        [FromQuery]
        [Display(Name = "limit")]
        public int Limit { get; set; }

        [Required]
        [FromQuery]
        [Display(Name = "offset")]
        public int Offset { get; set; }
    }
}
