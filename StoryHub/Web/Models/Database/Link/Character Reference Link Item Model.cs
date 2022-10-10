using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class CharacterReferenceLinkModel : BaseModel
    {
        [Key]
        public override int ID { get; set; }

        [Required]
        int CharacterID { get; set; } = 0;

        [Required]
        int ReferenceID { get; set; } = 0;
    }
}
