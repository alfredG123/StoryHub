using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class CharacterDataModel : BaseModel
    {
        [Key]
        public override int ID { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;
    }
}
