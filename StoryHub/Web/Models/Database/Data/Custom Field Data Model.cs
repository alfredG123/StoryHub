using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class CustomFieldDataModel : BaseModel
    {
        [Key]
        public override int ID { get; set; }

        [Required]
        public string FieldName { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;
    }
}
