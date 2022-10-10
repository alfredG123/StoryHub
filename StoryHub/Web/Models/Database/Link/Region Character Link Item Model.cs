using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class RegionCharacterLinkModel : BaseModel
    {
        [Key]
        public override int ID { get; set; }

        [Required]
        int RegionID { get; set; } = 0;

        [Required]
        int CharacterID { get; set; } = 0;
    }
}
