using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class PlotPlotLinkItemModel : BaseModel
    {
        [Key]
        public override int ID { get; set; }

        [Required]
        public int PlotID { get; set; } = 0;

        [Required]
        public int SubPlotID { get; set; } = 0;
    }
}
