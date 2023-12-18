using Common.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Dto
{

    public class AddVehiclePhotosRequestDto
    {
        [Required]
        [Display(Name = "id")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "frontPhoto")]
        public IFormFile FrontPhoto { get; set; }

        [Required]
        [Display(Name = "rearPhoto")]
        public IFormFile RearPhoto { get; set; }

        [Required]
        [Display(Name = "leftSidePhoto")]
        public IFormFile LeftSidePhoto { get; set; }

        [Required]
        [Display(Name = "rightSidePhoto")]
        public IFormFile RightSidePhoto { get; set; }

        [Required]
        [Display(Name = "interierPhoto1")]
        public IFormFile InterierPhoto1 { get; set; }

        [Required]
        [Display(Name = "interierPhoto2")]
        public IFormFile InterierPhoto2 { get; set; }

        [Required]
        [Display(Name = "technicalCertificate1")]
        public IFormFile TechnicalCertificate1 { get; set; }

        [Display(Name = "technicalCertificate2")]
        public IFormFile TechnicalCertificate2 { get; set; }

        [Required]
        [Display(Name = "insurance")]
        public IFormFile Insurance { get; set; }
    }
}
