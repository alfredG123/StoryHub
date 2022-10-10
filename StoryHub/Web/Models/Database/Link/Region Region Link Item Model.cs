using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class RegionRegionLinkModel : BaseModel
    {
        [Key]
        public override int ID { get; set; }

        [Required]
        int RegionID { get; set; } = 0;

        [Required]
        int SubRegionID { get; set; } = 0;
    }
}
