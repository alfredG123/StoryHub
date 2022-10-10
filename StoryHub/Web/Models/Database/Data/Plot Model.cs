using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class PlotModel : BaseModel
    {
        [Key]
        public override int ID { get; set; }
    }
}
