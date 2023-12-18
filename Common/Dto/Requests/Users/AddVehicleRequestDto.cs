using Common.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Dto
{
    public class AddVehicleRequestDto
    {
        [Required]
        [Display(Name = "name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "registrationSign")]
        public string RegistrationSign { get; set; }

        [Required]
        [Display(Name = "VIN")]
        public string VIN { get; set; }

        [Required]
        [Display(Name = "stkExpired")]
        public DateTimeOffset STKExpired { get; set; }

        [Required]
        [Display(Name = "yearOfManufacture")]
        public int YearOfManufacture { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        [Display(Name = "personsCapacity")]
        public int PersonsCapacity { get; set; }

        [Required]
        [Display(Name = "euro")]
        public EuroStandard Euro { get; set; }

        [Required]
        [Display(Name = "amenities")]
        public List<Amenities> Amenities { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        [Display(Name = "handicappedUserCount")]
        public int HandicappedUserCount { get; set; }
    }
}
