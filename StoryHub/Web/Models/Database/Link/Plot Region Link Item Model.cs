using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class PlotRegionLinkModel : BaseModel
    {
        [Key]
        public override int ID { get; set; }

        [Required]
        int PlotID { get; set; } = 0;

        [Required]
        int RegionID { get; set; } = 0;

        [Required]
        string Description { get; set; } = string.Empty;
    }
}
