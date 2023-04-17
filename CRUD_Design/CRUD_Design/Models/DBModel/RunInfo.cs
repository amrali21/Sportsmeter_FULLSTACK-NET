using System.ComponentModel.DataAnnotations;

namespace CRUD_Design.Models.DBModel
{
    public class RunInfo
    {
        public string? Id { get; set; }    
        [Required]
        public float Distance { get; set; }
        [Required]
        public float Time { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public virtual string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}
