using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class PlotPlotLinkModel : BaseModel
    {
        [Key]
        public override int ID { get; set; }

        [Required]
        int PlotID { get; set; } = 0;

        [Required]
        int SubPlotID { get; set; } = 0;
    }
}
