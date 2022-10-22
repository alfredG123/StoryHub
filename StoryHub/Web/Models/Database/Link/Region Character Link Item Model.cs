using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class RegionCharacterLinkItemModel : BaseModel
    {
        [Key]
        public override int ID { get; set; }

        [Required]
        public int RegionID { get; set; } = 0;

        [Required]
        public int CharacterID { get; set; } = 0;
    }
}
