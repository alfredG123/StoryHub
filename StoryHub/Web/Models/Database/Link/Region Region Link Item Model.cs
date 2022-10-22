using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class RegionRegionLinkItemModel : BaseModel
    {
        [Key]
        public override int ID { get; set; }

        [Required]
        public int RegionID { get; set; } = 0;

        [Required]
        public int SubRegionID { get; set; } = 0;
    }
}
