using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Dto
{
    public class TransferInfoRequestDto
    {
        [Display(Name = "bankNumber")]
        public string BankNumber { get; set; } = "";

        [Display(Name = "iban")]
        public string IBAN { get; set; } = "";

        [Display(Name = "swift")]
        public string SWIFT { get; set; } = "";
    }
}
