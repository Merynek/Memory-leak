using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Dto
{
    public class VehicleTransportVerificationRequestDto
    {
        [Required]
        [Display(Name = "id")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "isVerifiedForTransporting")]
        public bool IsVerifiedForTransporting { get; set; }       

    }
}
