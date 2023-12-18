using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Dto
{
    public class TransporterRequirementsPhotosRequestDto
    {        
        [Display(Name = "concessionDocuments")]
        public IFormFile? ConcessionDocuments { get; set; }

        [Display(Name = "businessRiskInsurance")]
        public IFormFile? BusinessRiskInsurance { get; set; }

    }
}
