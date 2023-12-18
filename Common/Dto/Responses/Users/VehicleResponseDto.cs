using Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Common.Dto
{
    public class VehicleResponseDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public bool IsVerifiedForTransporting { get; set; }
        [Required]
        public string RegistrationSign { get; set; }
        [Required]
        public string VIN { get; set; }
        [Required]
        public DateTimeOffset STKExpired { get; set; }
        [Required]
        public int YearOfManufacture { get; set; }
        [Required]
        public int PersonsCapacity { get; set; }
        [Required]
        public EuroStandard euro { get; set; }
        [Required]
        public ICollection<Amenities> Amenities { get; set; }
        [Required]
        public int HandicappedUserCount { get; set; }
        public PhotoResponseDto FrontPhoto { get; set; }
        public PhotoResponseDto RearPhoto { get; set; }
        public PhotoResponseDto LeftSidePhoto { get; set; }
        public PhotoResponseDto RightSidePhoto { get; set; }
        public PhotoResponseDto InterierPhoto1 { get; set; }
        public PhotoResponseDto InterierPhoto2 { get; set; }
        public PhotoResponseDto TechnicalCertificate1 { get; set; }
        public PhotoResponseDto TechnicalCertificate2 { get; set; }
        public PhotoResponseDto Insurance { get; set; }
    }
}
