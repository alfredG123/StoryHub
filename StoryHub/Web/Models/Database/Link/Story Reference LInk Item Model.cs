using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class StoryRegionLinkItemModel : BaseModel
    {
        [Key]
        public override int ID { get; set; }

        [Required]
        public int StoryID { get; set; } = 0;

        [Required]
        public int RegionID { get; set; } = 0;
    }
}
