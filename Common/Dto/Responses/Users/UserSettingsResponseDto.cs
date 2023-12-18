using Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Dto
{
    public class UserSettingsResponseDto
    {
        [Required]
        [Display(Name = "name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "surname")]
        public string Surname { get; set; }

        [Required]
        [Display(Name = "phoneNumber")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "ico")]
        public string Ico { get; set; }

        [Required]
        [Display(Name = "dic")]
        public string Dic { get; set; }

        [Required]
        [Display(Name = "notifications")]
        public ICollection<Notifications> Notifications { get; set; }

        [Required]
        [Display(Name = "isCompany")]
        public bool IsCompany { get; set; }

        [Required]
        [Display(Name = "isVerifiedForTransporting")]
        public bool IsVerifiedForTransporting { get; set; }

        [Display(Name = "address")]
        public UserAddressResponseDto Address { get; set; }

        [Display(Name = "mailingAddress")]
        public UserAddressResponseDto MailingAddress { get; set; }

        [Display(Name = "transferInfo")]
        public TransferInfoResponseDto TransferInfo { get; set; }

        [Display(Name = "transporterRequirements")]
        public TransporterRequirementsResponseDto TransporterRequirements { get; set; }
    }
}
