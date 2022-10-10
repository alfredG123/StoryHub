using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class PlotCharacterLinkModel : BaseModel
    {
        [Key]
        public override int ID { get; set; }

        [Required]
        int PlotID { get; set; } = 0;

        [Required]
        int CharacterID { get; set; } = 0;
    }
}
