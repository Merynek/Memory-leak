using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Dto
{
    public class TransporterRequirementsResponseDto
    {
        [Display(Name = "concessionNumber")]
        public string ConcessionNumber { get; set; }

        [Display(Name = "concessionDocuments")]
        public PhotoResponseDto? ConcessionDocuments { get; set; }

        [Display(Name = "businessRiskInsurance")]
        public PhotoResponseDto? BusinessRiskInsurance { get; set; }
    }
}
