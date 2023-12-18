using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Dto
{
    public class AdminUserDetailResponseDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public bool Active { get; set; }
        [Required]
        public bool Banned { get; set; }
        [Required]
        public bool IsVerifiedForTransporting { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Ico { get; set; }
        [Required]
        public string Dic { get; set; }
        [Required]
        public bool IsCompany { get; set; }
        public UserAddressResponseDto Address { get; set; }
        public UserAddressResponseDto MailingAddress { get; set; }
        public TransferInfoResponseDto TransferInfo { get; set; }
        public TransporterRequirementsResponseDto TransporterRequirements { get; set; }
        [Required]
        public ICollection<VehicleResponseDto> Vehicles { get; set; }
    }
}
