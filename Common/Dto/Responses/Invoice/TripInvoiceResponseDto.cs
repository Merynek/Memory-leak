using Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Dto
{
    public class TripInvoiceResponseDto
    {
        [Required]
        [Display(Name = "id")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "variableSymbol")]
        public string VariableSymbol { get; set; }

        [Required]
        [Display(Name = "dateOfIssue")]
        public DateTimeOffset DateOfIssue { get; set; }

        [Required]
        [Display(Name = "dueDate")]
        public DateTimeOffset DueDate { get; set; }

        [Required]
        [Display(Name = "type")]
        public InvoiceType Type { get; set; }

        [Required]
        [Display(Name = "price")]
        public PriceDto Price { get; set; }

        [Required]
        [Display(Name = "payed")]
        public bool Payed { get; set; }

        [Required]
        [Display(Name = "payType")]
        public TripInvoicePayType PayType { get; set; }      
    }
}
