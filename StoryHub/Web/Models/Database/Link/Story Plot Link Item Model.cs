using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class StoryPlotLinkModel : BaseModel
    {
        [Key]
        public override int ID { get; set; }

        [Required]
        int StoryID { get; set; } = 0;

        [Required]
        int PlotID { get; set; } = 0;
    }
}
