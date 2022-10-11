using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class ReferenceModel : BaseModel
    {
        [Key]
        public override int ID { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;
    }
}
