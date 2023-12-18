using Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace Common.Dto
{
    public class ErrorResponseDto
    {
        [Required]
        public ErrorCode ErrorCode { get; set; }
        [Required]
        public string Message { get; set; }
    }

}
