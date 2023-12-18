using Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Dto
{
    public class PriceDto
    {
        [Required]
        [Range(0, double.MaxValue)]
        [Display(Name = "amount")]        
        public decimal Amount { get; set; }

        [Required]
        [Display(Name = "currency")]
        public Currency Currency { get; set; }
        public PriceDto(decimal amount, Currency currency)
        {
            this.Amount = amount;
            this.Currency = currency;
        }
    }
}
