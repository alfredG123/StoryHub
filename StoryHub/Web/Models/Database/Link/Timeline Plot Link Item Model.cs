using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class TimelinePlotLinkItemModel : BaseModel
    {
        [Key]
        public override int ID { get; set; }

        [Required]
        public int TimelineID { get; set; } = 0;

        [Required]
        public int PlotID { get; set; } = 0;
    }
}
