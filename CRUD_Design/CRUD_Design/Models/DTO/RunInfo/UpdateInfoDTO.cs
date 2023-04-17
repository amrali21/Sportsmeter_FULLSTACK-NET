using System.ComponentModel.DataAnnotations;

namespace Sportsmeter_frontend.Models
{
    public class UpdateInfoDTO: GenericRunInfoDTO
    {
        [Required]
        [Range(0,10000, ErrorMessage ="invalid distance")]
        public float Distance { get; set; }

        [Required]
        [Range(0, 1000, ErrorMessage = "Invalid Time")]
        public float Time { get; set; }
    }
}
