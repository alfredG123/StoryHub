using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class PlotRegionLinkItemModel : BaseModel
    {
        [Key]
        public override int ID { get; set; }

        [Required]
        public int PlotID { get; set; } = 0;

        [Required]
        public int RegionID { get; set; } = 0;

        [Required]
        public string Description { get; set; } = string.Empty;
    }
}
