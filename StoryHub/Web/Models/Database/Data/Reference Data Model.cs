using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class ReferenceDataModel : BaseModel
    {
        [Key]
        public override int ID { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string RelatedURL { get; set; } = string.Empty;
    }
}
