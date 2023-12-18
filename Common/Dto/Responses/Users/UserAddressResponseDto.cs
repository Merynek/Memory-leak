using Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Dto
{
    public class UserAddressResponseDto
    {        
        public Country? Country { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string PSC { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string HouseNumber { get; set; }
    }
}
