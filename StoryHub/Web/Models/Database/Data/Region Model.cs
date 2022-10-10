using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class RegionModel : BaseModel
    {
        [Key]
        public override int ID { get; set; }
    }
}
