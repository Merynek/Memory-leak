using System.ComponentModel.DataAnnotations;

namespace Common.Dto
{
    public class BanUserRequestDto
    {
        [Required]
        [Display(Name = "idUser")]
        public int IdUser { get; set; }

        [Required]
        [Display(Name = "ban")]
        public bool Ban { get; set; }

    }
}
