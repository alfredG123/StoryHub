using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class StoryRegionLinkModel : BaseModel
    {
        [Key]
        public override int ID { get; set; }

        [Required]
        int StoryID { get; set; } = 0;

        [Required]
        int RegionID { get; set; } = 0;
    }
}
