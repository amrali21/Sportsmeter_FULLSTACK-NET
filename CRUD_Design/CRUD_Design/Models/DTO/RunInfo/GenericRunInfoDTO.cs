using System.ComponentModel.DataAnnotations;

namespace Sportsmeter_frontend.Models
{ 
    public class GenericRunInfoDTO
    {
        public string Id{ get; set; }
        [Required]
        public float Distance { get; set; }
        [Required]
        public float Time { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public virtual string ApplicationUserId { get; set; }
    }
}
