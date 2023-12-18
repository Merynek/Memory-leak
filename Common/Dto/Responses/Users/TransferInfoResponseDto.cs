using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Dto
{
    public class TransferInfoResponseDto
    {
        [Required]
        public string BankNumber { get; set; }
        [Required]
        public string IBAN { get; set; }
        [Required]
        public string SWIFT { get; set; }
    }
}
