using Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Dto
{
    public class UserSettingsRequestDto
    {
        [Display(Name = "name")]
        public string Name { get; set; } = "";

        [Display(Name = "surname")]
        public string Surname { get; set; } = "";

        [Display(Name = "phoneNumber")]
        public string PhoneNumber { get; set; } = "";

        [Display(Name = "ico")]
        public string Ico { get; set; } = "";

        [Display(Name = "dic")]
        public string Dic { get; set; } = "";

        [Display(Name = "isCompany")]
        public bool IsCompany { get; set; }

        [Display(Name = "notifications")]
        public ICollection<Notifications> Notifications { get; set; } = new List<Notifications> { };

        [Display(Name = "address")]
        public UserAddressRequestDto Address { get; set; }

        [Display(Name = "mailingAddress")]
        public UserAddressRequestDto MailingAddress { get; set; }

        [Display(Name = "transferInfo")]
        public TransferInfoRequestDto TransferInfo { get; set; }

        [Display(Name = "concessionNumber")]
        public string? ConcessionNumber { get; set; }
    }
}
