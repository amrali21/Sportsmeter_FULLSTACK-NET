using System.ComponentModel.DataAnnotations;

namespace Sportsmeter_frontend.Models
{
    public class CreateRunInfoDTO
    { 
        [Required]
        [Range(0, 10000, ErrorMessage ="Invalid Distance")]
        public float Distance { get; set; }

        [Required]
        [Range(0, 1000, ErrorMessage = "Invalid Time")]
        public float Time { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}
