using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class RegionReferenceLinkModel : BaseModel
    {
        [Key]
        public override int ID { get; set; }

        [Required]
        int RegionID { get; set; } = 0;

        [Required]
        int ReferenceID { get; set; } = 0;
    }
}
