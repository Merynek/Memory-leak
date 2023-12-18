using Common.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Dto
{
    public class UpdateVehicleRequestDto
    {
        [Required]
        [Display(Name = "id")]
        public int Id { get; set; }

        [Display(Name = "name")]
        public string? Name { get; set; }

        [Required]
        [Display(Name = "registrationSign")]
        public string? RegistrationSign { get; set; }

        [Required]
        [Display(Name = "VIN")]
        public string? VIN { get; set; }

        [Required]
        [Display(Name = "stkExpired")]
        public DateTimeOffset? STKExpired { get; set; }

        [Required]
        [Display(Name = "yearOfManufacture")]
        public int? YearOfManufacture { get; set; }

        [Range(1, int.MaxValue)]
        [Display(Name = "personsCapacity")]
        public int? PersonsCapacity { get; set; }

        [Display(Name = "euro")]
        public EuroStandard? Euro { get; set; }

        [Display(Name = "amenities")]
        public List<Amenities>? Amenities { get; set; }

        [Display(Name = "handicappedUserCount")]
        public int? HandicappedUserCount { get; set; }
    }

    public class UpdateVehiclePhotosRequestDto
    {
        [Required]
        [Display(Name = "id")]
        public int Id { get; set; }

        [Display(Name = "frontPhoto")]
        public IFormFile? FrontPhoto { get; set; }

        [Display(Name = "rearPhoto")]
        public IFormFile? RearPhoto { get; set; }

        [Display(Name = "leftSidePhoto")]
        public IFormFile? LeftSidePhoto { get; set; }

        [Display(Name = "rightSidePhoto")]
        public IFormFile? RightSidePhoto { get; set; }

        [Display(Name = "interierPhoto1")]
        public IFormFile? InterierPhoto1 { get; set; }

        [Display(Name = "interierPhoto2")]
        public IFormFile? InterierPhoto2 { get; set; }

        [Display(Name = "technicalCertificate1")]
        public IFormFile? TechnicalCertificate1 { get; set; }

        [Display(Name = "technicalCertificate2")]
        public IFormFile? TechnicalCertificate2 { get; set; }

        [Display(Name = "insurance")]
        public IFormFile? Insurance { get; set; }
    }
}
